using AutoMapper;
using DemoApp.Dto.Products;
using DemoApp.Interfaces;
using DemoApp.LoggerHub;
using DemoApp.Models;
using DemoApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IMapper _mapper = unitOfWork.Mapper;
    private readonly IProductRepository _productRepository = unitOfWork.ProductRepository;

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        var products = _mapper.Map<IList<ProductDto>>(await _productRepository.GetAllAsync());

        _ = unitOfWork.LoggerHub.Clients.All.OnLog(new LogMessage(0, $"Ping SignalR"));
        return Ok(products);
    }

    [HttpGet("{productId:int:min(1)}")]
    public async Task<ActionResult<ProductDto>> GetById([FromRoute] int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null)
            return NotFound();

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [Authorize(Roles = RolesConstants.Admin)]
    [HttpPost("{categoryId:int:min(1)}")]
    public async Task<ActionResult<ProductDto>> Create([FromRoute] int categoryId,
        [FromBody] CreateProductDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await unitOfWork.CategoryRepository.AnyAsync(categoryId))
        {
            _ = unitOfWork.LoggerHub.Clients.All.OnLog(new LogMessage(1,
                $"Failed to create a Product for category {categoryId}"));
            return NotFound(new { message = "Category not found" });
        }

        var product = _mapper.Map<Product>(productDto);
        product.CategoryId = categoryId;

        await _productRepository.AddAsync(product, categoryId);

        await unitOfWork.SaveAsync();
        _ = unitOfWork.LoggerHub.Clients.All.OnLog(new LogMessage(0, $"Product {product.Name} created"));

        return CreatedAtAction(nameof(GetById), new { productId = product.Id }, _mapper.Map<ProductDto>(product));
    }

    [Authorize(Roles = RolesConstants.Admin)]
    [HttpPut("{productId:int:min(1)}")]
    public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] UpdateProductDto toUpdate)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = await _productRepository.UpdateAsync(productId, _mapper.Map<Product>(toUpdate));
        if (product is null)
            return NotFound();

        await unitOfWork.SaveAsync();

        _ = unitOfWork.LoggerHub.Clients.All.OnLog(new LogMessage(0, $"Product {product.Id} updated"));
        return Ok(_mapper.Map<ProductDto>(product));
    }


    [Authorize(Roles = RolesConstants.Admin)]
    [HttpDelete("{productId:int:min(1)}")]
    public async Task<IActionResult> Delete([FromRoute] int productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null)
            return NotFound();

        _productRepository.Delete(product);
        await unitOfWork.SaveAsync();

        _ = unitOfWork.LoggerHub.Clients.All.OnLog(new LogMessage(0, $"Product {product.Id} deleted"));
        return NoContent();
    }
}