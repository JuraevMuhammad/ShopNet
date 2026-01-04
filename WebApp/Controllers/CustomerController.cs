using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("api/[controller]")]

public class CustomerController(ICustomerService service) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        var res = service.GetAllCustomers();
        return Ok(res);
    }

    [HttpGet("id")]
    public IActionResult GetCustomerById(int id)
    {
        var res = service.GetCustomerById(id);
        return Ok(res);
    }

    [HttpPost]
    public IActionResult AddCustomer(Customer customer)
    {
        var res = service.AddCustomer(customer);
        return Ok(res);
    }

    [HttpPut]
    public IActionResult UpdateCustomer(Customer customer)
    {
        var res = service.UpdateCustomer(customer);
        return Ok(res);
    }

    [HttpDelete]
    public IActionResult DeleteCustomer(int id)
    {
        var res = service.DeleteCustomer(id);
        return Ok(res);
    }
}