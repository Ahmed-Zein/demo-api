using AutoMapper;
using DemoApp.Dto.Products;
using DemoApp.Interfaces;
using DemoApp.Models;
using DemoApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IMapper _mapper = unitOfWork.Mapper;
    private readonly IProductRepository _productRepository = unitOfWork.ProductRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = _mapper.Map<IList<ProductDto>>(await _productRepository.GetAllAsync());
        return Ok(products);
    }

    [HttpGet("{productId:int:min(1)}")]
    public async Task<ActionResult<ProductDto>> GetById([FromRoute] int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null) return NotFound();
        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPost("{categoryId:int:min(1)}")]
    public async Task<ActionResult<Product>> Create([FromRoute] int categoryId, [FromBody] CreateProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await unitOfWork.CategoryRepository.AnyAsync(categoryId))
            return NotFound(new { message = "Category not found" });

        var product = _mapper.Map<Product>(productDto);
        product.CategoryId = categoryId;
        await _productRepository.AddAsync(product);
        await unitOfWork.SaveAsync();
        return Ok(_mapper.Map<Product>(product));
    }

    [HttpDelete("{productId:int:min(1)}")]
    public async Task<IActionResult> Delete([FromRoute] int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) return NotFound();
        _productRepository.Delete(product);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}