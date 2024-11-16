using AutoMapper;
using DemoApp.Dto.Categories;
using DemoApp.Interfaces;
using DemoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IMapper _mapper = unitOfWork.Mapper;
    private readonly ICategoryRepository _categoryRepository = unitOfWork.CategoryRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<CategoryDto>> GetAll([FromRoute] int id)
    {
        if (!await _categoryRepository.AnyAsync(id))
            return BadRequest(new { message = "Category not found." });
        var categories = await unitOfWork.CategoryRepository.GetByIdAsync(id);
        return Ok(_mapper.Map<CategoryDto>(categories));
    }

    [HttpPost]
    [Authorize(Roles = RolesConstants.Admin)]
    public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.AddAsync(category);
        await unitOfWork.SaveAsync();
        return Ok(_mapper.Map<CategoryDto>(category));
    }

    [Authorize(Roles = RolesConstants.Admin)]
    [HttpPut("{id:int:min(1)}")]
    public async Task<ActionResult<CategoryDto>> Put([FromRoute] int id, UpdateCategoryDto toUpdate)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            return NotFound();
        category.Name = toUpdate.Name;
        await unitOfWork.SaveAsync();
        return Ok(_mapper.Map<CategoryDto>(category));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = RolesConstants.Admin)]
    public async Task<ActionResult<CategoryDto>> Delete(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) return NotFound();
        _categoryRepository.Delete(category);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}