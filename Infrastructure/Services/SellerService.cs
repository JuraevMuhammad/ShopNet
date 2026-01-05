using Domain.Entities;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Services;

public class SellerService : ISellerService
{
    private readonly string _connectionString =
        "Server=localhost;Database=shopnet;Username=postgres;Password=12345;";
    
    public List<Seller> GetAllSellers()
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = "SELECT * FROM sellers";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        var res = command.ExecuteReader();
        var sellers = new List<Seller>();
        while (res.Read())
        {
            var seller = new Seller()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                FirstName = res.GetString(res.GetOrdinal("firstname")),
                LastName = res.GetString(res.GetOrdinal("lastname")),
                ShopName = res.GetString(res.GetOrdinal("shop_name")),
                Email = res.GetString(res.GetOrdinal("email")),
            };
            sellers.Add(seller);
        }
        connection.Close();
        return sellers;
    }

    public Seller GetSellerById(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = "SELECT * FROM sellers WHERE id = @id";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        var res = command.ExecuteReader();
        if (res.Read())
        {
            var seller = new Seller()
            {
                Id = res.GetInt32(res.GetOrdinal("id")),
                FirstName = res.GetString(res.GetOrdinal("firstname")),
                LastName = res.GetString(res.GetOrdinal("lastname")),
                ShopName = res.GetString(res.GetOrdinal("shop_name")),
                Email = res.GetString(res.GetOrdinal("email")),
            };
            return seller;
        }
        connection.Close();
        return null;
    }

    public bool AddSeller(Seller seller)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"insert into sellers(id, firstname, lastname, shop_name, email) values(@id, @firstname, @lastname, @shop_name, @email);";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", seller.Id);
        command.Parameters.AddWithValue("@firstname", seller.FirstName);
        command.Parameters.AddWithValue("@lastname", seller.LastName);
        command.Parameters.AddWithValue("@shop_name", seller.ShopName);
        command.Parameters.AddWithValue("@email", seller.Email);
        var res = command.ExecuteNonQuery();
        connection.Close();
        return res > 0;
    }

    public bool UpdateSeller(Seller seller)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"update sellers set firstname = @firstname, lastname = @lastname, shop_name = @shop_name, email = @email where id = @id;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", seller.Id);
        command.Parameters.AddWithValue("@firstname", seller.FirstName);
        command.Parameters.AddWithValue("@lastname", seller.LastName);
        command.Parameters.AddWithValue("@shop_name", seller.ShopName);
        command.Parameters.AddWithValue("@email", seller.Email);
        var res = command.ExecuteNonQuery();
        connection.Close();
        return res > 0;
    }

    public bool DeleteSeller(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        const string query = @"delete from sellers where id = @id;";
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        var res = command.ExecuteNonQuery();
        connection.Close();
        return res > 0;
    }
}