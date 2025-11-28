namespace OnlineGamesStore.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
