using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetHunters.Models;

namespace PlanetHunters.Import.DTO
{
    public class DiscoveryDTO
    {
        public DiscoveryDTO()
        {
            this.Stars = new HashSet<string>();
            this.Planets = new HashSet<string>();
            this.Pioneers = new HashSet<string>();
            this.Observers = new HashSet<string>();
        }
        public DateTime DateMade { get; set; }
        public string Telescope { get; set; }
        public virtual ICollection<string> Stars { get; set; }
        public virtual ICollection<string> Planets { get; set; }
        public virtual ICollection<string> Pioneers { get; set; }
        public virtual ICollection<string> Observers { get; set; }

    }
}
