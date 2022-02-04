using GameShop.Database;
using GameShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Service;

public class CartService
{
    public AppDbContext _context { get; set; }
    public string CartId { get; set; }
    public List<Cart> Carts { get; set; }


    public CartService(AppDbContext context)
    {
        _context = context;
    }


    public static CartService GetCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetService<AppDbContext>();

        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        session.SetString("CartId", cartId);

        return new CartService(context) {CartId = cartId};
    }


    public void AddItem(Game game)
    {
        var item = _context.Carts.FirstOrDefault(i => i.Game.Id == game.Id && i.CartId == CartId);
        if (item == null)
        {
            item = new Cart()
            {
                CartId = CartId,
                Game = game,
                Amount = 1
            };
            _context.Carts.Add(item);
        }
        else
        {
            item.Amount++;
        }

        _context.SaveChanges();
    }

    public void RemoveItem(Game game)
    {
        var item = _context.Carts.FirstOrDefault(i => i.Game.Id == game.Id && i.CartId == CartId);

        if (item != null)
        {
            if (item.Amount > 1)
            {
                item.Amount--;
            }
        }
        else
        {
            _context.Carts.Remove(item);
        }

        _context.SaveChanges();
    }

    public List<Cart> GetCartItems()
    {
        return Carts ?? (Carts = _context.Carts.Where(c => c.CartId == CartId)
            .Include(c => c.Game).ToList());
    }
    
    public double GetTotal() => _context.Carts.Where(n => n.CartId == CartId)
        .Select(c => c.Game.Price * c.Amount).Sum();


    public async Task Clear()
    {
        var items =  await _context.Carts
            .Where(c => c.CartId == CartId)
            .ToListAsync();
        _context.Carts.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}