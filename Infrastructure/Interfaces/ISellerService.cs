using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface ISellerService
{
    List<Seller> GetAllSellers();
    Seller GetSellerById(int id);
    bool AddSeller(Seller seller);
    bool UpdateSeller(Seller seller);
    bool DeleteSeller(int id);
}