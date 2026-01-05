using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    List<Product> GetAllProducts();
    Product GetProductById(int id);
    bool AddProduct(Product product);
    bool UpdateProduct(Product product);
    bool DeleteProduct(int id);
    List<Product> SearchProducts(string search);
    List<Product> GetProductsByCategoryId(int categoryId);
    List<Product> GetProductsBySellerId(int sellerId);
    List<Product> GetLowStockProducts(int amount = 5);
    List<Product> GetTopProductsByQuantity(int count = 5);
}