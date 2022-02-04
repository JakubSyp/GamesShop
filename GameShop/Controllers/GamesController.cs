using GameShop.Enums;
using GameShop.Models;
using GameShop.Service;
using GameShop.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Controllers;
[Authorize(Roles = UserRoles.Admin)]
public class Games : Controller
{
    private readonly IGamesService _service;

    public Games(IGamesService service)
    {
        _service = service;
    }
    [AllowAnonymous]
    public  async Task<IActionResult> Index()
    {
        var games = await _service.GetAll();
        return View(games);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(NewGame gameModel)
    {
        if (!ModelState.IsValid) return View(gameModel);
        await _service.Add(gameModel);
        return RedirectToAction(nameof(Index));
    }
    [AllowAnonymous]

    public async Task<IActionResult> Details(int id)
    {
        var game = await _service.GetById(id);
        if (game == null) return View("ErrorNotFound");
        return View(game);
    }

    public async Task<IActionResult> Update(int id )
    {
        var game = await _service.GetById(id);
        if (game == null) return View("ErrorNotFound");


        var updatedGame = new NewGame()
        {
            Id = game.Id,
            Title = game.Title,
            Description = game.Description,
            GameType = game.GameType,
            Platform = game.Platform,
            ReleaseDate = game.ReleaseDate,
            Price = game.Price,
            Image = game.Image,
            Language = game.Language
        };
        return View(updatedGame);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, NewGame gameModel)
    {
        if (id != gameModel.Id) return View("ErrorNotFound");

        if (!ModelState.IsValid) return View(gameModel);
        await _service.Update(gameModel);
        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Delete(int id)
    {
        var game = await _service.GetById(id);
        if (game == null) return View("ErrorNotFound");
        return View(game);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        var game = await _service.GetById(id);
        if (game == null) return View("ErrorNotFound");
        await _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    [AllowAnonymous]
    public async  Task<IActionResult> GetByPlatform(Platform platform)
    {
        var games = await _service.GetByPlatforms(platform);
        if (games.ToList().Count == 0  ) return View("NoGames");
        return View("Index", games);
    }
    [AllowAnonymous]
    public async Task<IActionResult> GetBytype(GameType gameType)
    {
        var games = await _service.GetByType(gameType);
        if (games.ToList().Count == 0  ) return View("NoGames");
        return View("Index", games);
    }
    
} 