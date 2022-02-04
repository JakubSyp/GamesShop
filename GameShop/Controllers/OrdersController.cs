using System.Security.Claims;
using GameShop.Models;
using GameShop.Service;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Controllers;

public class OrdersController : Controller
{

    private readonly IGamesService _service;
    private readonly CartService _cart;
    private readonly IOrdersService _orders;


    public OrdersController(IGamesService service, CartService cart, IOrdersService orders)
    {
        _service = service;
        _cart = cart;
        _orders = orders;
    }


    public async Task<IActionResult> Index()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        string userRole = User.FindFirstValue(ClaimTypes.Role);
        var orders =  await _orders.GetOrders(userId, userRole);
        return View(orders); 
    }
    public IActionResult ShoppingCart()
    {
        var items = _cart.GetCartItems();
        _cart.Carts = items;
        var result = new ShoppingCart()
        {
            CartService = _cart,
            Total = _cart.GetTotal()
        };
        return View(result);
    }


    public async Task<IActionResult> AddToCart(int id)
    {
        var item = await _service.GetById(id);
        if (item != null)
        {
            _cart.AddItem(item);
        }

        return RedirectToAction(nameof(ShoppingCart));
    }

    public async Task<IActionResult> RemoveItem(int id)
    {
        var item = await _service.GetById(id);
        if (item != null)
        {
            _cart.RemoveItem(item);
        }

        return RedirectToAction(nameof(ShoppingCart));
    }

    public async Task<IActionResult> CompleteOrder()
    {
        var items = _cart.GetCartItems();
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        string userEmailAdress = User.FindFirstValue(ClaimTypes.Email);
        await _orders.StoreOrder(items, userId, userEmailAdress);
        await _cart.Clear();
        return View("OrderCompleted");
    }
}