using GameShop.Enums;
using GameShop.Models;

namespace GameShop.Service;

public interface IGamesService
{
    Task<Game> GetById(int id);
    Task<IEnumerable<Game>> GetAll();
    Task Add(NewGame gameModel);
    Task Delete(int id );
    Task Update(NewGame gameModel);
    Task<IEnumerable<Game>> GetByPlatforms(Platform platform);
    Task<IEnumerable<Game>> GetByType(GameType gameType);
}