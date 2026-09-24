namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.Carts;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    /// <summary>
    /// Giriş yapan kullanıcının detaylı sepetini (ürün adı, resim, fiyat, satıcı ve toplam tutar) getirir
    /// GET: api/carts/my-cart
    /// </summary>
    [HttpGet("my-cart")]
    public IActionResult GetMyCart()
    {
        var result = _cartService.GetMyCart();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Sepete ürün varyantı ekler veya adedini artırır
    /// POST: api/carts/add-item
    /// </summary>
    [HttpPost("add-item")]
    public IActionResult AddItem([FromBody] AddToCartDto dto)
    {
        var result = _cartService.AddItemToCart(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Kullanıcının sepetini tamamen boşaltır
    /// POST: api/carts/clear
    /// </summary>
    [HttpPost("clear")]
    public IActionResult Clear()
    {
        var result = _cartService.ClearCart();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _cartService.GetAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _cartService.GetById(id);
        if (result.Success) return Ok(result);
        return NotFound(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateCartDto dto)
    {
        var result = _cartService.Add(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateCartDto dto)
    {
        var result = _cartService.Update(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _cartService.Delete(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}
