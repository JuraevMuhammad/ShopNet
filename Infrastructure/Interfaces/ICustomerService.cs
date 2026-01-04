using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
    List<Customer> GetAllCustomers();
    Customer GetCustomerById(int id);
    bool AddCustomer(Customer customer);
    bool UpdateCustomer(Customer customer);
    bool DeleteCustomer(int id);
}