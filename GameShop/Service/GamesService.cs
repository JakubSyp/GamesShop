using GameShop.Controllers;
using GameShop.Database;
using GameShop.Enums;
using GameShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Service;

public class GamesService:IGamesService
{
    private readonly AppDbContext _context;

    public GamesService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<Game> GetById(int id)
    {
        var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        return game;
    }

    public async Task<IEnumerable<Game>> GetAll()
    {
       var games = await _context.Set<Game>().ToListAsync();
       return games;
    }

    public async Task Add(NewGame gameModel)
    {
        var newGame = new Game()
        {
            Image = gameModel.Image,
            Title = gameModel.Title,
            Description = gameModel.Description,
            GameType = gameModel.GameType,
            Platform = gameModel.Platform,
            ReleaseDate = gameModel.ReleaseDate,
            Price = gameModel.Price,
            Language = gameModel.Language
        };
        await _context.Games.AddAsync(newGame);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var deletedGame = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        _context.Games.Remove(deletedGame);
        await _context.SaveChangesAsync();
    }

    public  async Task Update(NewGame modelGame)
    {
        var dbGame = await _context.Games.FirstOrDefaultAsync(g => g.Id == modelGame.Id);
        if (dbGame != null)
        {
            dbGame.Image = modelGame.Image;
            dbGame.Title = modelGame.Title;
            dbGame.Description = modelGame.Description;
            dbGame.GameType = modelGame.GameType;
            dbGame.Platform = modelGame.Platform;
            dbGame.ReleaseDate = modelGame.ReleaseDate;
            dbGame.Price = modelGame.Price;
            dbGame.Language = modelGame.Language;
        }

        await _context.SaveChangesAsync();
    }

    public  async Task<IEnumerable<Game>> GetByPlatforms(Platform platform)
    {
        var games = await _context.Games.Where(g => g.Platform == platform).ToListAsync();
        return games;

    }
    public  async Task<IEnumerable<Game>> GetByType(GameType gameType)
    {
        var games = await _context.Games.Where(g => g.GameType == gameType).ToListAsync();
        return games;

    }
}