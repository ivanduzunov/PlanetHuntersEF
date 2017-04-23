using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PlanetHunters.Models
{
    public class Star
    {
        private int temperature;
        [Key]
        public int Id { get; set; }
        [Required, MaxLength (255)]
        public string Name { get; set; }
        [Required]
        public int Temperature
        {
            get
            {
                return temperature;
            }
            set
            {
                int checkTemp = value;
                if (checkTemp >= 2400)
                {
                    this.temperature = value;
                }
                else
                {
                    throw new Exception("The Temperature cannot be lower than 2400K)!");
                }
            }
        }
        public int HostStarSystemId { get; set; }
        [Required]
        public virtual StarSystem HostStarSystem { get; set; }
        public int? DiscoveryId { get; set; }
        public virtual Discovery Discovery { get; set; }
    }
}
