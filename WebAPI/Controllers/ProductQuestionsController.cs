namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.ProductQuestions;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductQuestionsController : ControllerBase
{
    private readonly IProductQuestionService _service;

    public ProductQuestionsController(IProductQuestionService service)
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

    /// <summary>
    /// Tüm soruları sayfalanmış olarak listeler
    /// GET: api/productquestions/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("paged")]
    public IActionResult GetPaged([FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _service.GetPaged(pageRequest);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// Ürüne ait soruları sayfalanmış olarak listeler
    /// GET: api/productquestions/product/5/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("product/{productId:int}/paged")]
    public IActionResult GetPagedByProductId(int productId, [FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _service.GetPagedByProductId(productId, pageRequest);
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
    public IActionResult Add([FromBody] CreateProductQuestionDto dto)
    {
        var result = _service.Add(dto);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateProductQuestionDto dto)
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
