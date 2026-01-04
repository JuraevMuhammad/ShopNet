using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]

public class CategoryController(ICategoryService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var result = service.GetAllCategories();
        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetCategoryById(int id)
    {
        var result = service.GetCategoryById(id);
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddCategory(Category category)
    {
        bool result = service.AddCategory(category);
        return Ok(result);
    }

    [HttpPut]
    public IActionResult UpdateCategory(Category category)
    {
        bool result = service.UpdateCategory(category);
        return Ok(result);
    }

    [HttpDelete]
    public IActionResult DeleteCategory(int id)
    {
        bool result = service.DeleteCategory(id);
        return Ok(result);
    }
}

