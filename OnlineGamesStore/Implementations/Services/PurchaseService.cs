using Microsoft.Data.SqlClient;
using OnlineGamesStore.Dtos;
using OnlineGamesStore.Implementations.Services.Interfaces;
using OnlineGamesStore.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

public class PurchaseService : IPurchaseService
{
    private readonly OnlineGamesStoreDbContext _context;

    public PurchaseService(OnlineGamesStoreDbContext context)
    {
        _context = context;
    }

    public async Task AddSaleAsync(AddSaleDto dto)
    {
        try
        {
            var sql = "EXEC usr_ins_sale @GameId, @UserId, @Quantity, @Price";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@GameId", dto.GameId),
                new SqlParameter("@UserId", dto.UserId),
                new SqlParameter("@Quantity", dto.Quantity),
                new SqlParameter("@Price", dto.Price)
            );
        }
        catch (SqlException ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}
