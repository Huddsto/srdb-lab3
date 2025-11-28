namespace OnlineGamesStore.Dtos
{
    public class MostExpensiveGameDto
    {
        public string Message { get; set; }
        public string Назва_гри { get; set; }
        public int? Код_покупки { get; set; }
        public decimal? Ціна { get; set; }
        public int? Кількість { get; set; }
        public decimal? Загальна_сума { get; set; }
        public string Покупець { get; set; }
        public string Розробник { get; set; }
    }
}
