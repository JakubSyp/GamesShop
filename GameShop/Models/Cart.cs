using System.ComponentModel.DataAnnotations;
using GameShop.Database;
using Microsoft.AspNetCore.Identity;

namespace GameShop.Models;

public class Cart
{
    [Key] public int Id { get; set; }

    public Game Game { get; set; }

    public int Amount { get; set; }
    public string CartId { get; set; }
}