using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGamesStore.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string PasswordHash { get; set; } 
        public string Role { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
