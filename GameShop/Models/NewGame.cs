using System.ComponentModel.DataAnnotations;
using GameShop.Enums;

namespace GameShop.Models;

public class NewGame
{
    public int Id { get; set; }
    
    [Display(Name = "Image")]
    [Required(ErrorMessage = "Image is required")]
    public string Image { get; set; }
    
    [Display(Name = "Title")]
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    
    [Display(Name = "Description")]
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    
    [Display(Name = "Type")]
    [Required(ErrorMessage = "Type is required")]
    public GameType GameType { get; set; }
    
    [Display(Name = "Platform")]
    [Required(ErrorMessage = "Platform is required")]
    public Platform Platform { get; set; }
    
    [Display(Name = "Relase date")]
    [Required(ErrorMessage = "Release date is required")]
    public DateTime ReleaseDate { get; set; }
    
    [Display(Name = "Price")]
    [Required(ErrorMessage = "Price is required")]
    public double Price { get; set; }
    
    [Display(Name = "Language")]
    [Required(ErrorMessage = "Language is required")]
    public string Language { get; set; }
    
}

