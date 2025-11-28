namespace OnlineGamesStore.Dtos
{
    public class AddSaleDto
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
