namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.Brands;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandsController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _brandService.GetAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Markaları sayfalanmış olarak listeler
    /// GET: api/brands/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("paged")]
    public IActionResult GetPaged([FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _brandService.GetPaged(pageRequest);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _brandService.GetById(id);
        if (result.Success) return Ok(result);
        return NotFound(result);
    }

    [HttpGet("my-brands")]
    public IActionResult GetMyBrands()
    {
        var result = _brandService.GetMyBrands();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Giriş yapan satıcının markalarını sayfalanmış olarak listeler
    /// GET: api/brands/my-brands/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("my-brands/paged")]
    public IActionResult GetMyBrandsPaged([FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _brandService.GetMyBrandsPaged(pageRequest);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("user/{userId:int}")]
    public IActionResult GetByUserId(int userId)
    {
        var result = _brandService.GetListByUserId(userId);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateBrandDto dto)
    {
        var result = _brandService.Add(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateBrandDto dto)
    {
        var result = _brandService.Update(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _brandService.Delete(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}
