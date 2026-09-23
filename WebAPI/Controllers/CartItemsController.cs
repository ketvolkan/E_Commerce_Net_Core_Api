namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.CartItems;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CartItemsController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemsController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _cartItemService.GetAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _cartItemService.GetById(id);
        if (result.Success) return Ok(result);
        return NotFound(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateCartItemDto dto)
    {
        var result = _cartItemService.Add(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateCartItemDto dto)
    {
        var result = _cartItemService.Update(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _cartItemService.Delete(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}
