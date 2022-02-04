using GameShop.Database;
using GameShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Service;

public class OrdersService:IOrdersService
{
    private readonly AppDbContext _context;


    public OrdersService(AppDbContext context)
    {
        _context = context;
    }
    
    
    public async Task StoreOrder(List<Cart> items, string userId, string userEmailAdress)
    {
        var order = new Order()
        {
            UserId = userId,
            Email = userEmailAdress
        };
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        foreach (var item in items)
        {
            var orderItem = new OrderItem()
            {
                Amount = item.Amount,
                GameId = item.Game.Id,
                OrderId = order.Id,
                Price = item.Game.Price
            };
            await _context.OrdersItems.AddAsync(orderItem);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetOrders(string userId, string userRole)
    {
        var orders = await _context.Orders
            .Include(n => n.Orderitems)
            .ThenInclude(n => n.Game)
            .ToListAsync();

        if (userRole != "Admin")
        {
            orders = orders.Where(n => n.UserId == userId).ToList();
        }
        return orders;
    }
}