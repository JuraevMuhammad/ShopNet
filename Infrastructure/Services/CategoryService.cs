using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly string _connectionString = 
            "Server=localhost;Database=shopnet;Username=postgres;Password=12345;";

    public List<Category> GetAllCategories()
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = "SELECT * FROM categories";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        var result = command.ExecuteReader();
        List<Category> categories = new List<Category>();
        while (result.Read())
        {
            var category = new Category()
            {
                Id = result.GetInt32(result.GetOrdinal("id")),
                Name = result.GetString(result.GetOrdinal("name")),
                Description = result.GetString(result.GetOrdinal("description")),
            };
            categories.Add(category);
        }
        connection.Close();
        return categories;
    }

    public Category GetCategoryById(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string quety = @"select * from categories where id = @id;";
        NpgsqlCommand command = new NpgsqlCommand(quety, connection);
        command.Parameters.AddWithValue("@id", id);
        var result = command.ExecuteReader();
        if (result.Read())
        {
            var category = new Category()
            {
                Id = result.GetInt32(result.GetOrdinal("id")),
                Name = result.GetString(result.GetOrdinal("name")),
                Description = result.GetString(result.GetOrdinal("description")),
            };
            return category;
        }
        connection.Close();
        return null;
    }

    public bool AddCategory(Category category)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = "insert into categories(id, name, description) values(@id, @name, @description);";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", category.Id);
        command.Parameters.AddWithValue("@name", category.Name);
        command.Parameters.AddWithValue("@description", category.Description);
        var result = command.ExecuteNonQuery();
        connection.Close();
        return result > 0;
    }

    public bool UpdateCategory(Category category)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = "update categories set name = @name, description = @description where id = @id;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", category.Id);
        command.Parameters.AddWithValue("@name", category.Name);
        command.Parameters.AddWithValue("@description", category.Description);
        var result = command.ExecuteNonQuery();
        connection.Close();
        return result > 0;
    }

    public bool DeleteCategory(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"delete from categories where id = @id;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        var result = command.ExecuteNonQuery();
        connection.Close();
        return result > 0;
    }
}