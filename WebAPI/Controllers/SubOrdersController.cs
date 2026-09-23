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

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _subOrderService.GetAll();
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
