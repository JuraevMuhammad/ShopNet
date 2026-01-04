using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly string _connectionString = 
            "Server=localhost;Database=shopnet;Username=postgres;Password=12345;";

    public List<Customer> GetAllCustomers()
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = "SELECT * FROM customers";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        var res = command.ExecuteReader();
        var customers = new List<Customer>();
        while (res.Read())
        {
            var customer = new Customer()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                FirstName = res.GetString(res.GetOrdinal("firstname")),
                LastName = res.GetString(res.GetOrdinal("lastname")),
                Email = res.GetString(res.GetOrdinal("email")),
                Phone = res.GetString(res.GetOrdinal("phone")),
            };
            customers.Add(customer);
        }
        connection.Close();
        return customers;
    }

    public Customer GetCustomerById(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = "SELECT * FROM customers WHERE id = @id";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        var res = command.ExecuteReader();
        if (res.Read())
        {
            var customer = new Customer()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                FirstName = res.GetString(res.GetOrdinal("firstname")),
                LastName = res.GetString(res.GetOrdinal("lastname")),
                Email = res.GetString(res.GetOrdinal("email")),
                Phone = res.GetString(res.GetOrdinal("phone")),
            };
            return customer;
        }
        connection.Close();
        return null;
    }

    public bool AddCustomer(Customer customer)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"insert into customers(id, firstname, lastname, email, phone) values(@id, @firstname, @lastname, @email, @phone);";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", customer.Id);
        command.Parameters.AddWithValue("@firstname", customer.FirstName);
        command.Parameters.AddWithValue("@lastname", customer.LastName);
        command.Parameters.AddWithValue("@email", customer.Email);
        command.Parameters.AddWithValue("@phone", customer.Phone);
        var res = command.ExecuteNonQuery();
        connection.Close();
        return res > 0;
    }

    public bool UpdateCustomer(Customer customer)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"update customers set firstname = @firstname, lastname = @lastname, email = @email, phone = @phone where id = @id;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", customer.Id);
        command.Parameters.AddWithValue("@firstname", customer.FirstName);
        command.Parameters.AddWithValue("@lastname", customer.LastName);
        command.Parameters.AddWithValue("@email", customer.Email);
        command.Parameters.AddWithValue("@phone", customer.Phone);
        var res = command.ExecuteNonQuery();
        connection.Close();
        return res > 0;
    }

    public bool DeleteCustomer(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"delete from customers where id = @id;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        var res = command.ExecuteNonQuery();
        connection.Close();
        return res > 0;
    }
}