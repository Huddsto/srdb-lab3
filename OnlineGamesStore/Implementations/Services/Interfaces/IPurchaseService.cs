using OnlineGamesStore.Dtos;

namespace OnlineGamesStore.Implementations.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task AddSaleAsync(AddSaleDto dto);
    }

}
