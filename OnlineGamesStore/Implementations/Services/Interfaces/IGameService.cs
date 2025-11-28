using OnlineGamesStore.Dtos;

namespace OnlineGamesStore.Implementations.Services.Interfaces
{
    public interface IGameService
    {
        Task<GameDetailsDto> GetGameDetailsAsync(int id);
    }
}
