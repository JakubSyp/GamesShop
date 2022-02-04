using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GameShop.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    public string Email { get; set; }
    public string UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    
    public IdentityUser User { get; set; }
    
    public List<OrderItem> Orderitems { get; set; }
}