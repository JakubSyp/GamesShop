using GameShop.Service;

namespace GameShop.Models;

public class ShoppingCart
{
    public CartService CartService { get; set; }
    public double Total { get; set; }
}