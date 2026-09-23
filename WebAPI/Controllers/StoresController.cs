namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.Stores;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _storeService.GetAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _storeService.GetById(id);
        if (result.Success) return Ok(result);
        return NotFound(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] CreateStoreDto dto)
    {
        var result = _storeService.Add(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateStoreDto dto)
    {
        var result = _storeService.Update(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _storeService.Delete(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}
