using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]

public class ProductController(IProductService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = service.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("id")]
    public IActionResult GetProductById(int id)
    {
        var res = service.GetProductById(id);
        return Ok(res);
    }

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        var res = service.AddProduct(product);
        return Ok(res);
    }

    [HttpPut]
    public IActionResult UpdateProduct(Product product)
    {
        var res = service.UpdateProduct(product);
        return Ok(res);
    }

    [HttpDelete]
    public IActionResult DeleteProduct(int id)
    {
        var res = service.DeleteProduct(id);
        return Ok(res);
    }

    [HttpGet("search")]
    public IActionResult SearchProducts(string search)
    {
        var res = service.SearchProducts(search);
        return Ok(res);
    }

    [HttpGet("category-id")]
    public IActionResult GetProductsByCategoryId(int categoryId)
    {
        var res = service.GetProductsByCategoryId(categoryId);
        return Ok(res);
    }

    [HttpGet("seller-id")]
    public IActionResult GetProductsBySellerId(int sellerId)
    {
        var res = service.GetProductsBySellerId(sellerId);
        return Ok(res);
    }

    [HttpGet("amount")]
    public IActionResult GetLowStockProducts(int amount)
    {
        var res = service.GetLowStockProducts(amount);
        return Ok(res);
    }

    [HttpGet("top-quantity")]
    public IActionResult GetTopProductsByQuantity(int count)
    {
        var res = service.GetTopProductsByQuantity(count);
        return Ok(res);
    }
}

