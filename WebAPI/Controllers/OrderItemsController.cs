namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.OrderItems;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemsController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _orderItemService.GetAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _orderItemService.GetById(id);
        if (result.Success) return Ok(result);
        return NotFound(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateOrderItemDto dto)
    {
        var result = _orderItemService.Add(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateOrderItemDto dto)
    {
        var result = _orderItemService.Update(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _orderItemService.Delete(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}
