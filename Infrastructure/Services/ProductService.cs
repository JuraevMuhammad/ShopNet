using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly string _connectionString = 
        "Server=localhost;Database=shopnet;Username=postgres;Password=12345;";
    
    public List<Product> GetAllProducts()
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = "SELECT * FROM products";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        var res = command.ExecuteReader();
        var products = new List<Product>();
        while (res.Read())
        {
            var product = new Product()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                Name = res.GetString(res.GetOrdinal("name")),
                Price = res.GetDecimal(res.GetOrdinal("price")),
                Quantity = res.GetInt32(res.GetOrdinal("quantity")),
                CategoryId = res.GetInt32(res.GetOrdinal("category_id")),
                SellerId = res.GetInt32(res.GetOrdinal("seller_id")),
            };
            products.Add(product);
        }
        connection.Close();
        return products;
    }

    public Product GetProductById(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = "SELECT * FROM products WHERE id = @id";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        var res = command.ExecuteReader();
        if (res.Read())
        {
            var product = new Product()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                Name = res.GetString(res.GetOrdinal("name")),
                Price = res.GetDecimal(res.GetOrdinal("price")),
                Quantity = res.GetInt32(res.GetOrdinal("quantity")),
                CategoryId = res.GetInt32(res.GetOrdinal("category_id")),
                SellerId = res.GetInt32(res.GetOrdinal("seller_id"))
            };
            return product;
        }
        connection.Close();
        return null;
    }

    public bool AddProduct(Product product)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"insert into products(id, name, price, quantity, category_id, seller_id) values(@id, @name, @price, @quantity, @category_id, @seller_id);";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", product.Id);
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@price", product.Price);
        command.Parameters.AddWithValue("@quantity", product.Quantity);
        command.Parameters.AddWithValue("@category_id", product.CategoryId);
        command.Parameters.AddWithValue("@seller_id", product.SellerId);
        var res = command.ExecuteNonQuery();
        connection.Close();
        return res > 0;
    }

    public bool UpdateProduct(Product product)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"update products set name = @name, price = @price, quantity = @quantity, category_id = @category_id, seller_id = @seller_id where id = @id;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", product.Id);
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@price", product.Price);
        command.Parameters.AddWithValue("@quantity", product.Quantity);
        command.Parameters.AddWithValue("@category_id", product.CategoryId);
        command.Parameters.AddWithValue("@seller_id", product.SellerId);
        var res = command.ExecuteNonQuery();
        connection.Close();
        return res > 0;
    }

    public bool DeleteProduct(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"delete from products where id = @id;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        var res = command.ExecuteNonQuery();
        connection.Close();
        return res > 0;
    }

    public List<Product> SearchProducts(string search)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"select * from products where name like '%' || @name || '%';";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@name", search);
        var res = command.ExecuteReader();
        var products = new List<Product>();
        while (res.Read())
        {
            var product = new Product()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                Name = res.GetString(res.GetOrdinal("name")),
                Price = res.GetDecimal(res.GetOrdinal("price")),
                Quantity = res.GetInt32(res.GetOrdinal("quantity")),
                CategoryId = res.GetInt32(res.GetOrdinal("category_id")),
                SellerId = res.GetInt32(res.GetOrdinal("seller_id"))
            };
            products.Add(product);
        }
        connection.Close();
        return products;
    }

    public List<Product> GetProductsByCategoryId(int categoryId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"select * from products where category_id = @category_id;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@category_id", categoryId);
        var res = command.ExecuteReader();
        var products = new List<Product>();
        while (res.Read())
        {
            var product = new Product()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                Name = res.GetString(res.GetOrdinal("name")),
                Price = res.GetDecimal(res.GetOrdinal("price")),
                Quantity = res.GetInt32(res.GetOrdinal("quantity")),
                CategoryId = res.GetInt32(res.GetOrdinal("category_id")),
                SellerId = res.GetInt32(res.GetOrdinal("seller_id"))
            };
            products.Add(product);
        }
        connection.Close();
        return products;
    }

    public List<Product> GetProductsBySellerId(int sellerId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"select * from products where seller_id = @seller_id;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@seller_id", sellerId);
        var res = command.ExecuteReader();
        var products = new List<Product>();
        while (res.Read())
        {
            var product = new Product()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                Name = res.GetString(res.GetOrdinal("name")),
                Price = res.GetDecimal(res.GetOrdinal("price")),
                Quantity = res.GetInt32(res.GetOrdinal("quantity")),
                CategoryId = res.GetInt32(res.GetOrdinal("category_id")),
                SellerId = res.GetInt32(res.GetOrdinal("seller_id"))
            };
            products.Add(product);
        }
        connection.Close();
        return products;
    }

    public List<Product> GetLowStockProducts(int amount = 5)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"select * from products as p order by p.quantity limit @amount;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@amount", amount);
        var res = command.ExecuteReader();
        var products = new List<Product>();
        while (res.Read())
        {
            var product = new Product()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                Name = res.GetString(res.GetOrdinal("name")),
                Price = res.GetDecimal(res.GetOrdinal("price")),
                Quantity = res.GetInt32(res.GetOrdinal("quantity")),
                CategoryId = res.GetInt32(res.GetOrdinal("category_id")),
                SellerId = res.GetInt32(res.GetOrdinal("seller_id"))
            };
            products.Add(product);
        }

        return products;
    }

    public List<Product> GetTopProductsByQuantity(int count = 5)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"select * from products as p order by p.quantity desc limit @count;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@count", count);
        var res = command.ExecuteReader();
        var products = new List<Product>();
        while (res.Read())
        {
            var product = new Product()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                Name = res.GetString(res.GetOrdinal("name")),
                Price = res.GetDecimal(res.GetOrdinal("price")),
                Quantity = res.GetInt32(res.GetOrdinal("quantity")),
                CategoryId = res.GetInt32(res.GetOrdinal("category_id")),
                SellerId = res.GetInt32(res.GetOrdinal("seller_id"))
            };
            products.Add(product);
        }

        return products;
    }
}