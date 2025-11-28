using Microsoft.EntityFrameworkCore;
using OnlineGamesStore.Dtos;
using OnlineGamesStore.Implementations.Services.Interfaces;
using OnlineGamesStore.Persistence.DbContext;

namespace OnlineGamesStore.Implementations.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly OnlineGamesStoreDbContext _context;

        public StatisticsService(OnlineGamesStoreDbContext context)
        {
            _context = context;
        }

        // Get count of games priced below the average
        public async Task<int> GetGamesLessThanAvgAsync()
        {
            var result = await _context.Database
                .SqlQuery<ScalarFunctionsDtos>($"SELECT dbo.Games_Less_Than_Avg() AS Count")
                .FirstAsync();

            return result.Count;
        }

        // Get count of games below a specified price
        public async Task<int> GetGamesCountByPriceAsync(decimal price)
        {
            var result = await _context.Database
                .SqlQuery<ScalarFunctionsDtos>($"SELECT dbo.Count_Games_By_Price({price}) AS Count")
                .FirstAsync();

            return result.Count;
        }

        // Get the max order by game name (table-valued function)
        public async Task<List<MaxOrderByNameDto>> GetMaxOrderByGameNameAsync(string gameName)
        {
            return await _context.Database
                .SqlQuery<MaxOrderByNameDto>($"SELECT * FROM dbo.GetMaxOrderByGameName({gameName})")
                .ToListAsync();
        }

        // Get the most expensive game for a given date (table-valued function)
        public async Task<List<MostExpensiveGameByDateDto>> GetMostExpensiveGameByDateAsync(DateTime date)
        {
            return await _context.Database
                .SqlQuery<MostExpensiveGameByDateDto>($"SELECT * FROM dbo.GetMostExpensiveGameByDate('{date:yyyy-MM-dd}')")
                .ToListAsync();
        }
    }
}
