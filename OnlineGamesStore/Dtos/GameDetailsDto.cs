namespace OnlineGamesStore.Dtos
{
    public class GameDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Platform { get; set; }
        public string DeveloperName { get; set; }

        public List<GamePurchaseDto> Purchases { get; set; } = new();
    }

    public class GamePurchaseDto
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Buyer { get; set; }
    }

}
