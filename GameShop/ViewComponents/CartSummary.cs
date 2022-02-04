using GameShop.Models;
using GameShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.ViewComponents;
[ViewComponent]
public class CartSummary:ViewComponent
{
    private readonly CartService _cartService;


    public CartSummary(CartService cartService)
    {
        _cartService = cartService;
    }

    
    public IViewComponentResult Invoke()
    {
        var items = _cartService.GetCartItems();
        return View(items.Count);
    }
}