using OnlineGamesStore.Dtos;

namespace OnlineGamesStore.Implementations.Services.Interfaces
{
    public interface IDeveloperService
    {
        Task<DeveloperDto?> GetDeveloperByIdAsync(int id);
        Task<IEnumerable<DeveloperDto>> GetAllDevelopersAsync();
    }
}
