namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.Favorites;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;

    public FavoritesController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    /// <summary>
    /// Giriş yapan kullanıcının favori ürünlerini detaylı liste olarak getirir
    /// GET: api/favorites/my-favorites
    /// </summary>
    [HttpGet("my-favorites")]
    public IActionResult GetMyFavorites()
    {
        var result = _favoriteService.GetMyFavorites();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Ürünü favorilere ekler veya zaten favorideyse çıkarır (Toggle Heart)
    /// POST: api/favorites/toggle/5
    /// </summary>
    [HttpPost("toggle/{productId:int}")]
    public IActionResult Toggle(int productId)
    {
        var result = _favoriteService.ToggleFavorite(productId);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _favoriteService.GetAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _favoriteService.GetById(id);
        if (result.Success) return Ok(result);
        return NotFound(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateFavoriteDto dto)
    {
        var result = _favoriteService.Add(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateFavoriteDto dto)
    {
        var result = _favoriteService.Update(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _favoriteService.Delete(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}
