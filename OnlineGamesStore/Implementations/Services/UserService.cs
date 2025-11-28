using Microsoft.EntityFrameworkCore;
using OnlineGamesStore.Dtos;
using OnlineGamesStore.Implementations.Services.Interfaces;
using OnlineGamesStore.Persistence.DbContext;

namespace OnlineGamesStore.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly OnlineGamesStoreDbContext _context;

        public UserService(OnlineGamesStoreDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                PasswordHash = user.PasswordHash,
                Role = user.Role
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    PasswordHash = u.PasswordHash,
                    Role = u.Role
                })
                .ToListAsync();
        }
    }
}
