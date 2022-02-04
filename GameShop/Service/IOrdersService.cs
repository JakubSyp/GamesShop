using GameShop.Models;

namespace GameShop.Service;

public interface IOrdersService
{
    Task StoreOrder(List<Cart> items, string userId, string userEmailAdress);
    Task<List<Order>> GetOrders(string userId, string userRole);
}