namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.SubOrders;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SubOrdersController : ControllerBase
{
    private readonly ISubOrderService _subOrderService;

    public SubOrdersController(ISubOrderService subOrderService)
    {
        _subOrderService = subOrderService;
    }

    /// <summary>
    /// Giriş yapan satıcının mağazasına gelen siparişleri listeler
    /// GET: api/suborders/my-store-orders
    /// </summary>
    [HttpGet("my-store-orders")]
    public IActionResult GetMyStoreOrders()
    {
        var result = _subOrderService.GetMyStoreOrders();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Giriş yapan satıcının mağazasına gelen siparişleri sayfalanmış olarak listeler
    /// GET: api/suborders/my-store-orders/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("my-store-orders/paged")]
    public IActionResult GetMyStoreOrdersPaged([FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _subOrderService.GetMyStoreOrdersPaged(pageRequest);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _subOrderService.GetAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Tüm alt siparişleri sayfalanmış olarak listeler
    /// GET: api/suborders/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("paged")]
    public IActionResult GetPaged([FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _subOrderService.GetPaged(pageRequest);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _subOrderService.GetById(id);
        if (result.Success) return Ok(result);
        return NotFound(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateSubOrderDto dto)
    {
        var result = _subOrderService.Add(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateSubOrderDto dto)
    {
        var result = _subOrderService.Update(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _subOrderService.Delete(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}
