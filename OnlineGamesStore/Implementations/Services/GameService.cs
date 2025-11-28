using Microsoft.EntityFrameworkCore;
using OnlineGamesStore.Dtos;
using OnlineGamesStore.Implementations.Services.Interfaces;
using OnlineGamesStore.Persistence.DbContext;

public class GameService : IGameService
{
    private readonly OnlineGamesStoreDbContext _context;

    public GameService(OnlineGamesStoreDbContext context)
    {
        _context = context;
    }

    public async Task<GameDetailsDto> GetGameDetailsAsync(int id)
    {
        var game = await _context.Games
            .Include(g => g.Developer)
            .Include(g => g.Purchases)
                .ThenInclude(p => p.Users)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (game == null)
            return null;

        return new GameDetailsDto
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Genre = game.Genre,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
            Platform = game.Platform,
            DeveloperName = game.Developer?.Name,
            Purchases = game.Purchases.Select(p => new GamePurchaseDto
            {
                PurchaseId = p.Id,
                PurchaseDate = p.PurchaseDate,
                Quantity = p.Quantity,
                TotalPrice = p.TotalPrice,
                Buyer = p.Users.Name + " " + p.Users.Surname
            }).ToList()
        };
    }
}
