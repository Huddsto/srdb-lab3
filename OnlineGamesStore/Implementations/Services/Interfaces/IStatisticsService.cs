using OnlineGamesStore.Dtos;

namespace OnlineGamesStore.Implementations.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<int> GetGamesLessThanAvgAsync();
        Task<int> GetGamesCountByPriceAsync(decimal price);
        Task<List<MaxOrderByNameDto>> GetMaxOrderByGameNameAsync(string gameName);
        Task<List<MostExpensiveGameByDateDto>> GetMostExpensiveGameByDateAsync(DateTime date);
    }
}
