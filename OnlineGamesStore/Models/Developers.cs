using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGamesStore.Models
{
    public class Developers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public string Information { get; set; }
        public ICollection<Games> Games { get; set; }
    }
}
