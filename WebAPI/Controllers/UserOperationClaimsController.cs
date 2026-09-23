namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.UserOperationClaims;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : ControllerBase
{
    private readonly IUserOperationClaimService _service;

    public UserOperationClaimsController(IUserOperationClaimService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _service.GetAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _service.GetById(id);
        if (result.Success) return Ok(result);
        return NotFound(result);
    }

    [HttpPost]
    public IActionResult Add([FromBody] UserOperationClaimDto dto)
    {
        var result = _service.Add(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UserOperationClaimDto dto)
    {
        var result = _service.Update(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _service.Delete(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}
