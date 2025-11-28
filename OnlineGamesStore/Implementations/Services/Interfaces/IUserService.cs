using OnlineGamesStore.Dtos;

namespace OnlineGamesStore.Implementations.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
