using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]

public class SellerController(ISellerService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllSellers()
    {
        var res = service.GetAllSellers();
        return Ok(res);
    }

    [HttpGet("id")]
    public IActionResult GetSellerById(int id)
    {
        var res = service.GetSellerById(id);
        return Ok(res);
    }

    [HttpPost]
    public IActionResult AddSeller(Seller seller)
    {
        var res = service.AddSeller(seller);
        return Ok(res);
    }

    [HttpPut]
    public IActionResult UpdateSeller(Seller seller)
    {
        var res = service.UpdateSeller(seller);
        return Ok(res);
    }

    [HttpDelete]
    public IActionResult DeleteSeller(int id)
    {
        var res = service.DeleteSeller(id);
        return Ok(res);
    }
}