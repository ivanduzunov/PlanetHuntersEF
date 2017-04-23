using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PlanetHunters.Models
{
    public class Planet
    {
        private double mass { get; set; }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required]
        public double Mass
        {
       
            get
            {
                return mass; 
            }
            set
            {
                double checkMass = value;
                if (checkMass > 0)
                {
                    this.mass = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The mass cannot be zero or less!");
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
