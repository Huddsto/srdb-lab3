using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGamesStore.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime PurchaseDate {  get; set; }
        public int UserId {  get; set; }
        public Users Users { get; set; }
        public int GameId {  get; set; }
        public Games Games { get; set; }
        public int Quantity {  get; set; }
        public decimal TotalPrice {  get; set; }
    }
}
