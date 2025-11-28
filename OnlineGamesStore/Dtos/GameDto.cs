using OnlineGamesStore.Models;

namespace OnlineGamesStore.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Platform { get; set; }
        public int DeveloperId { get; set; }
        public Developers? Developer { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
