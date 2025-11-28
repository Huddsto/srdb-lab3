using Microsoft.EntityFrameworkCore;
using OnlineGamesStore.Dtos;
using OnlineGamesStore.Implementations.Services.Interfaces;
using OnlineGamesStore.Persistence.DbContext;

namespace OnlineGamesStore.Implementations.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly OnlineGamesStoreDbContext _context;

        public DeveloperService(OnlineGamesStoreDbContext context)
        {
            _context = context;
        }

        public async Task<DeveloperDto?> GetDeveloperByIdAsync(int id)
        {
            var developer = await _context.Developers.FirstOrDefaultAsync(d => d.Id == id);

            if (developer == null)
                return null;

            return new DeveloperDto
            {
                Id = developer.Id,
                Name = developer.Name,
                Country = developer.Country,
                Website = developer.Website,
                Information = developer.Information
            };
        }

        public async Task<IEnumerable<DeveloperDto>> GetAllDevelopersAsync()
        {
            return await _context.Developers
                .Select(d => new DeveloperDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Country = d.Country,
                    Website = d.Website,
                    Information = d.Information
                })
                .ToListAsync();
        }
    }
}
