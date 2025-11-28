using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGamesStore.Models
{
    public class PurchaseLog
    {
        public int Id { get; set; }
        public int PurcaseId {  get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
