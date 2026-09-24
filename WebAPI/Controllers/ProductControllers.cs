namespace WebAPI.Controllers;

using Business.Abstract;
using Entities.Dtos.Products;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Tüm ürünleri detaylarıyla listeler
    /// GET: api/products
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _productService.GetAll();
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Ürünleri sayfalanmış (paginated) olarak listeler
    /// GET: api/products/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("paged")]
    public IActionResult GetPaged([FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _productService.GetPaged(pageRequest);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Öne çıkan / son eklenen aktif ürünleri listeler (Ana sayfa vitrini)
    /// GET: api/products/featured
    /// </summary>
    [HttpGet("featured")]
    public IActionResult GetFeatured()
    {
        var result = _productService.GetFeaturedProducts();
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Ürün filtreleme ve arama (Kategori, marka, mağaza, fiyat ve sıralama)
    /// GET: api/products/search
    /// </summary>
    [HttpGet("search")]
    public IActionResult Search([FromQuery] ProductFilterDto filter)
    {
        var result = _productService.Search(filter);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// ID değerine göre tek bir ürün getirir
    /// GET: api/products/5
    /// </summary>
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _productService.GetById(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return NotFound(result);
    }

    /// <summary>
    /// Belirli bir kategoriye ait ürünleri listeler
    /// GET: api/products/category/2
    /// </summary>
    [HttpGet("category/{categoryId:int}")]
    public IActionResult GetByCategoryId(int categoryId)
    {
        var result = _productService.GetListByCategoryId(categoryId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Belirli bir kategoriye ait ürünleri sayfalanmış olarak listeler
    /// GET: api/products/category/2/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("category/{categoryId:int}/paged")]
    public IActionResult GetByCategoryIdPaged(int categoryId, [FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _productService.GetPagedByCategoryId(categoryId, pageRequest);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Giriş yapan satıcının kendi ürünlerini listeler
    /// GET: api/products/my-products
    /// </summary>
    [HttpGet("my-products")]
    public IActionResult GetMyProducts()
    {
        var result = _productService.GetMyProducts();
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Giriş yapan satıcının kendi ürünlerini sayfalanmış olarak listeler
    /// GET: api/products/my-products/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("my-products/paged")]
    public IActionResult GetMyProductsPaged([FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _productService.GetMyProductsPaged(pageRequest);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Belirli bir satıcıya ait ürünleri listeler
    /// GET: api/products/user/2
    /// </summary>
    [HttpGet("user/{userId:int}")]
    public IActionResult GetByUserId(int userId)
    {
        var result = _productService.GetListByUserId(userId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Belirli bir satıcıya ait ürünleri sayfalanmış olarak listeler
    /// GET: api/products/user/2/paged?pageNumber=1&pageSize=10
    /// </summary>
    [HttpGet("user/{userId:int}/paged")]
    public IActionResult GetByUserIdPaged(int userId, [FromQuery] Core.Utilities.Paging.PageRequest pageRequest)
    {
        var result = _productService.GetPagedByUserId(userId, pageRequest);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Yeni ürün ekler
    /// POST: api/products
    /// </summary>
    [HttpPost]
    public IActionResult Add([FromBody] CreateProductDto createProductDto)
    {
        var result = _productService.Add(createProductDto);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Mevcut ürünü günceller
    /// PUT: api/products
    /// </summary>
    [HttpPut]
    public IActionResult Update([FromBody] UpdateProductDto updateProductDto)
    {
        var result = _productService.Update(updateProductDto);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    /// <summary>
    /// Ürün siler
    /// DELETE: api/products/5
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _productService.Delete(id);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}