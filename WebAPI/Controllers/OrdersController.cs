namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Sepetteki ürünlerle tek tıkla sipariş oluşturur (Multi-Vendor SubOrder ve stok düşümü dahil)
    /// POST: api/orders/checkout
    /// </summary>
    [HttpPost("checkout")]
    public IActionResult Checkout([FromBody] CheckoutDto dto)
    {
        var result = _orderService.Checkout(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Giriş yapan kullanıcının tüm sipariş geçmişini kalemleriyle birlikte getirir
    /// GET: api/orders/my-orders
    /// </summary>
    [HttpGet("my-orders")]
    public IActionResult GetMyOrders()
    {
        var result = _orderService.GetMyOrders();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Giriş yapan kullanıcının sipariş geçmişini sayfalanmış olarak getirir
    /// GET: api/orders/my-orders/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("my-orders/paged")]
    public IActionResult GetMyOrdersPaged([FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _orderService.GetMyOrdersPaged(pageRequest);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Siparişin detayını (alt siparişler, ürünler, kargo takip) getirir
    /// GET: api/orders/5/detail
    /// </summary>
    [HttpGet("{id:int}/detail")]
    public IActionResult GetOrderDetail(int id)
    {
        var result = _orderService.GetOrderDetail(id);
        if (result.Success) return Ok(result);
        return NotFound(result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _orderService.GetAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Siparişleri sayfalanmış olarak listeler
    /// GET: api/orders/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("paged")]
    public IActionResult GetPaged([FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _orderService.GetPaged(pageRequest);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _orderService.GetById(id);
        if (result.Success) return Ok(result);
        return NotFound(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateOrderDto dto)
    {
        var result = _orderService.Add(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateOrderDto dto)
    {
        var result = _orderService.Update(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _orderService.Delete(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}
