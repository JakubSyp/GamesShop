using System.ComponentModel.DataAnnotations;
using GameShop.Enums;

namespace GameShop.Models;

public class Game
{
    [Key]
    public int Id { get; set; }
    
    public string Image { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public GameType GameType { get; set; }
    
    public Platform Platform { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public double Price { get; set; }
    
    public string Language { get; set; }
    
}


