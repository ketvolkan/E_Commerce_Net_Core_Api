namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.SubOrders;

using System.Linq;
using Core.Utilities.Paging;

public class SubOrderManager : ISubOrderService
{
    private readonly ISubOrderDal _subOrderDal;
    private readonly IStoreDal _storeDal;
    private readonly IOrderItemDal _orderItemDal;
    private readonly IMapper _mapper;

    public SubOrderManager(ISubOrderDal subOrderDal, IStoreDal storeDal, IOrderItemDal orderItemDal, IMapper mapper)
    {
        _subOrderDal = subOrderDal;
        _storeDal = storeDal;
        _orderItemDal = orderItemDal;
        _mapper = mapper;
    }

    public IDataResult<List<Entities.Dtos.Orders.SubOrderDetailDto>> GetMyStoreOrders()
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (userId <= 0) return new ErrorDataResult<List<Entities.Dtos.Orders.SubOrderDetailDto>>("Kullanıcı oturumu bulunamadı.");

        var stores = _storeDal.GetList(s => s.UserId == userId);
        if (!stores.Any())
        {
            return new SuccessDataResult<List<Entities.Dtos.Orders.SubOrderDetailDto>>(new List<Entities.Dtos.Orders.SubOrderDetailDto>());
        }

        var storeIds = stores.Select(s => s.Id).ToList();
        var subOrders = _subOrderDal.GetList(so => storeIds.Contains(so.StoreId));

        var dtos = subOrders.Select(so =>
        {
            var store = stores.FirstOrDefault(s => s.Id == so.StoreId);
            var items = _orderItemDal.GetList(oi => oi.SubOrderId == so.Id);

            return new Entities.Dtos.Orders.SubOrderDetailDto
            {
                Id = so.Id,
                StoreId = so.StoreId,
                StoreName = store?.Name ?? "Mağaza",
                SubOrderNumber = so.SubOrderNumber,
                TotalPrice = so.TotalPrice,
                Status = so.Status,
                CargoTrackingNumber = so.CargoTrackingNumber,
                CargoCompany = so.CargoCompany,
                CreatedDate = so.CreatedDate,
                Items = items.Select(oi => new Entities.Dtos.Orders.OrderItemDetailDto
                {
                    Id = oi.Id,
                    ProductVariantId = oi.ProductVariantId,
                    ProductName = oi.ProductName,
                    VariantInfo = oi.VariantInfo,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }).OrderByDescending(so => so.CreatedDate).ToList();

        return new SuccessDataResult<List<Entities.Dtos.Orders.SubOrderDetailDto>>(dtos);
    }

    public IDataResult<PagedResult<Entities.Dtos.Orders.SubOrderDetailDto>> GetMyStoreOrdersPaged(PageRequest pageRequest)
    {
        var result = GetMyStoreOrders();
        if (!result.Success)
        {
            return new ErrorDataResult<PagedResult<Entities.Dtos.Orders.SubOrderDetailDto>>(result.Message);
        }

        var paged = result.Data.ToPagedResult(pageRequest.PageNumber, pageRequest.PageSize);
        return new SuccessDataResult<PagedResult<Entities.Dtos.Orders.SubOrderDetailDto>>(paged);
    }

    public IDataResult<List<UpdateSubOrderDto>> GetAll()
    {
        // Admin can see all suborders
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            var list = _subOrderDal.GetList();
            return new SuccessDataResult<List<UpdateSubOrderDto>>(_mapper.Map<List<UpdateSubOrderDto>>(list));
        }

        // Store owners should see suborders for their stores; regular users see none here (they see Orders)
        var roles = Core.Utilities.Security.CurrentUser.GetRoles();
        if (roles.Contains("store.getall") || roles.Contains("store.getbyid"))
        {
            // find stores by current user id then filter suborders by those stores
            var userId = Core.Utilities.Security.CurrentUser.GetUserId();
            var list = _subOrderDal.GetList(so => so.Store != null && so.Store.UserId == userId);
            return new SuccessDataResult<List<UpdateSubOrderDto>>(_mapper.Map<List<UpdateSubOrderDto>>(list));
        }

        return new SuccessDataResult<List<UpdateSubOrderDto>>(new List<UpdateSubOrderDto>());
    }

    public IDataResult<PagedResult<UpdateSubOrderDto>> GetPaged(PageRequest pageRequest)
    {
        var result = GetAll();
        if (!result.Success)
        {
            return new ErrorDataResult<PagedResult<UpdateSubOrderDto>>(result.Message);
        }

        var paged = result.Data.ToPagedResult(pageRequest.PageNumber, pageRequest.PageSize);
        return new SuccessDataResult<PagedResult<UpdateSubOrderDto>>(paged);
    }

    public IDataResult<UpdateSubOrderDto> GetById(int id)
    {
        var entity = _subOrderDal.Get(s => s.Id == id);
        if (entity == null) return new ErrorDataResult<UpdateSubOrderDto>(Messages.SubOrderNotFound);
        return new SuccessDataResult<UpdateSubOrderDto>(_mapper.Map<UpdateSubOrderDto>(entity));
    }

    public IResult Add(CreateSubOrderDto createSubOrderDto)
    {
        // SubOrders are created by system/store after order placement; enforce store ownership if needed
        var entity = _mapper.Map<SubOrder>(createSubOrderDto);
        _subOrderDal.Add(entity);
        return new SuccessResult(Messages.SubOrderAdded);
    }

    public IResult Update(UpdateSubOrderDto updateSubOrderDto)
    {
        var existing = _subOrderDal.Get(s => s.Id == updateSubOrderDto.Id);
        if (existing == null) return new ErrorResult(Messages.SubOrderNotFoundForUpdate);
        _mapper.Map(updateSubOrderDto, existing);
        _subOrderDal.Update(existing);
        return new SuccessResult(Messages.SubOrderUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _subOrderDal.Get(s => s.Id == id);
        if (existing == null) return new ErrorResult(Messages.SubOrderNotFoundForDelete);
        _subOrderDal.Delete(existing);
        return new SuccessResult(Messages.SubOrderDeleted);
    }
}
