namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Orders;
using Business.BusinessAspects.Autofac;

using System;
using System.Linq;
using Core.Utilities.Paging;

public class OrderManager : IOrderService
{
    private readonly IOrderDal _orderDal;
    private readonly ISubOrderDal _subOrderDal;
    private readonly IOrderItemDal _orderItemDal;
    private readonly ICartDal _cartDal;
    private readonly ICartItemDal _cartItemDal;
    private readonly IProductVariantDal _productVariantDal;
    private readonly IMapper _mapper;

    public OrderManager(
        IOrderDal orderDal,
        ISubOrderDal subOrderDal,
        IOrderItemDal orderItemDal,
        ICartDal cartDal,
        ICartItemDal cartItemDal,
        IProductVariantDal productVariantDal,
        IMapper mapper)
    {
        _orderDal = orderDal;
        _subOrderDal = subOrderDal;
        _orderItemDal = orderItemDal;
        _cartDal = cartDal;
        _cartItemDal = cartItemDal;
        _productVariantDal = productVariantDal;
        _mapper = mapper;
    }

    public IDataResult<OrderDetailDto> Checkout(CheckoutDto checkoutDto)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (userId <= 0) return new ErrorDataResult<OrderDetailDto>("Kullanıcı oturumu bulunamadı.");

        var cart = _cartDal.GetCartWithDetails(userId);
        if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
        {
            return new ErrorDataResult<OrderDetailDto>("Sepetinizde ürün bulunmamaktadır.");
        }

        // Validate stock
        foreach (var item in cart.CartItems)
        {
            if (item.ProductVariant == null || item.ProductVariant.StockQuantity < item.Quantity)
            {
                var pName = item.ProductVariant?.Product?.Name ?? "Ürün";
                return new ErrorDataResult<OrderDetailDto>($"'{pName}' için yetersiz stok!");
            }
        }

        var orderNumber = "ORD-" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "-" + Random.Shared.Next(100, 999);
        decimal totalOrderPrice = cart.CartItems.Sum(ci =>
        {
            var price = ci.ProductVariant!.DiscountPrice > 0 ? ci.ProductVariant.DiscountPrice : ci.ProductVariant.Price;
            return price * ci.Quantity;
        });

        // 1. Create Main Order
        var order = new Order
        {
            UserId = userId,
            ShippingAddressId = checkoutDto.ShippingAddressId,
            BillingAddressId = checkoutDto.BillingAddressId ?? checkoutDto.ShippingAddressId,
            OrderNumber = orderNumber,
            TotalPrice = totalOrderPrice,
            PaymentStatus = "Paid",
            OrderStatus = "Processing",
            CreatedDate = DateTime.UtcNow
        };
        _orderDal.Add(order);

        // Fetch inserted order to get Id
        var createdOrder = _orderDal.Get(o => o.OrderNumber == orderNumber);
        if (createdOrder == null) return new ErrorDataResult<OrderDetailDto>("Sipariş oluşturulamadı.");

        // 2. Group items by Store for Multi-Vendor SubOrders
        var storeGroups = cart.CartItems.GroupBy(ci => ci.ProductVariant!.StoreId);

        foreach (var group in storeGroups)
        {
            var storeId = group.Key;
            decimal subOrderTotal = group.Sum(ci =>
            {
                var price = ci.ProductVariant!.DiscountPrice > 0 ? ci.ProductVariant.DiscountPrice : ci.ProductVariant.Price;
                return price * ci.Quantity;
            });

            var subOrderNumber = $"{orderNumber}-S{storeId}";
            var subOrder = new SubOrder
            {
                OrderId = createdOrder.Id,
                StoreId = storeId,
                SubOrderNumber = subOrderNumber,
                TotalPrice = subOrderTotal,
                Status = "Preparing",
                CreatedDate = DateTime.UtcNow
            };
            _subOrderDal.Add(subOrder);

            var createdSubOrder = _subOrderDal.Get(so => so.SubOrderNumber == subOrderNumber);
            if (createdSubOrder == null) continue;

            // 3. Create OrderItems and deduct stock
            foreach (var cartItem in group)
            {
                var variant = cartItem.ProductVariant!;
                var unitPrice = variant.DiscountPrice > 0 ? variant.DiscountPrice : variant.Price;

                var orderItem = new OrderItem
                {
                    SubOrderId = createdSubOrder.Id,
                    ProductVariantId = variant.Id,
                    ProductName = variant.Product?.Name ?? "Ürün",
                    VariantInfo = $"{variant.Color} / {variant.Size}".Trim(' ', '/'),
                    Quantity = cartItem.Quantity,
                    UnitPrice = unitPrice,
                    CommissionRate = 10
                };
                _orderItemDal.Add(orderItem);

                // Deduct stock
                variant.StockQuantity -= cartItem.Quantity;
                _productVariantDal.Update(variant);
            }
        }

        // 4. Clear Cart
        foreach (var cartItem in cart.CartItems)
        {
            _cartItemDal.Delete(cartItem);
        }

        return GetOrderDetail(createdOrder.Id);
    }

    public IDataResult<List<OrderDetailDto>> GetMyOrders()
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (userId <= 0) return new ErrorDataResult<List<OrderDetailDto>>("Kullanıcı oturumu bulunamadı.");

        var orders = _orderDal.GetOrdersWithDetails(userId);
        var dtos = orders.Select(MapToOrderDetailDto).ToList();
        return new SuccessDataResult<List<OrderDetailDto>>(dtos);
    }

    public IDataResult<PagedResult<OrderDetailDto>> GetMyOrdersPaged(PageRequest pageRequest)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (userId <= 0) return new ErrorDataResult<PagedResult<OrderDetailDto>>("Kullanıcı oturumu bulunamadı.");

        var orders = _orderDal.GetOrdersWithDetails(userId);
        var dtos = orders.Select(MapToOrderDetailDto).ToList();
        var paged = dtos.ToPagedResult(pageRequest.PageNumber, pageRequest.PageSize);
        return new SuccessDataResult<PagedResult<OrderDetailDto>>(paged);
    }

    public IDataResult<OrderDetailDto> GetOrderDetail(int id)
    {
        var order = _orderDal.GetOrderWithDetails(id);
        if (order == null) return new ErrorDataResult<OrderDetailDto>(Messages.OrderNotFound);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && order.UserId != userId)
        {
            return new ErrorDataResult<OrderDetailDto>(Messages.AuthorizationDenied);
        }

        return new SuccessDataResult<OrderDetailDto>(MapToOrderDetailDto(order));
    }

    private static OrderDetailDto MapToOrderDetailDto(Order order)
    {
        return new OrderDetailDto
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            TotalPrice = order.TotalPrice,
            PaymentStatus = order.PaymentStatus,
            OrderStatus = order.OrderStatus,
            CreatedDate = order.CreatedDate,
            ShippingAddressTitle = order.ShippingAddress?.Title ?? string.Empty,
            ShippingAddressCity = order.ShippingAddress?.City ?? string.Empty,
            ShippingAddressDistrict = order.ShippingAddress?.District ?? string.Empty,
            ShippingAddressDetail = order.ShippingAddress?.AddressDetail ?? string.Empty,
            SubOrders = order.SubOrders.Select(so => new SubOrderDetailDto
            {
                Id = so.Id,
                StoreId = so.StoreId,
                StoreName = so.Store?.Name ?? "Satıcı",
                SubOrderNumber = so.SubOrderNumber,
                TotalPrice = so.TotalPrice,
                Status = so.Status,
                CargoTrackingNumber = so.CargoTrackingNumber,
                CargoCompany = so.CargoCompany,
                CreatedDate = so.CreatedDate,
                Items = so.OrderItems.Select(oi => new OrderItemDetailDto
                {
                    Id = oi.Id,
                    ProductVariantId = oi.ProductVariantId,
                    ProductName = oi.ProductName,
                    VariantInfo = oi.VariantInfo,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            }).ToList()
        };
    }

    public IDataResult<List<OrderListDto>> GetAll()
    {
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            var list = _orderDal.GetList();
            return new SuccessDataResult<List<OrderListDto>>(_mapper.Map<List<OrderListDto>>(list));
        }

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var listByUser = _orderDal.GetList(o => o.UserId == userId);
        return new SuccessDataResult<List<OrderListDto>>(_mapper.Map<List<OrderListDto>>(listByUser));
    }

    public IDataResult<PagedResult<OrderListDto>> GetPaged(PageRequest pageRequest)
    {
        PagedResult<Order> paged;
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            paged = _orderDal.GetPagedList(pageRequest.PageNumber, pageRequest.PageSize);
        }
        else
        {
            var userId = Core.Utilities.Security.CurrentUser.GetUserId();
            paged = _orderDal.GetPagedList(pageRequest.PageNumber, pageRequest.PageSize, o => o.UserId == userId);
        }

        var dtos = _mapper.Map<List<OrderListDto>>(paged.Items);
        return new PagedDataResult<OrderListDto>(dtos, paged.TotalCount, paged.PageNumber, paged.PageSize);
    }

    public IDataResult<OrderListDto> GetById(int id)
    {
        var entity = _orderDal.Get(o => o.Id == id);
        if (entity == null) return new ErrorDataResult<OrderListDto>(Messages.OrderNotFound);
        return new SuccessDataResult<OrderListDto>(_mapper.Map<OrderListDto>(entity));
    }

    [SecuredOperation("order.add")]
    public IResult Add(CreateOrderDto createOrderDto)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var entity = _mapper.Map<Order>(createOrderDto);
        entity.UserId = userId;
        _orderDal.Add(entity);
        return new SuccessResult(Messages.OrderAdded);
    }

    public IResult Update(UpdateOrderDto updateOrderDto)
    {
        var existing = _orderDal.Get(o => o.Id == updateOrderDto.Id);
        if (existing == null) return new ErrorResult(Messages.OrderNotFound);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        // Admins can update any order; store owners may update order status for their stores via SubOrders handled elsewhere
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _mapper.Map(updateOrderDto, existing);
        _orderDal.Update(existing);
        return new SuccessResult(Messages.OrderUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _orderDal.Get(o => o.Id == id);
        if (existing == null) return new ErrorResult(Messages.OrderNotFound);
        _orderDal.Delete(existing);
        return new SuccessResult(Messages.OrderDeleted);
    }
}
