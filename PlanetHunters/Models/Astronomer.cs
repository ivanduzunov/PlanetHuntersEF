using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PlanetHunters.Models
{
    public class Astronomer
    {
        public Astronomer()
        {
            this.PioneeringDiscoveries = new HashSet<Discovery>();
            this.ObservationDiscoveries = new HashSet<Discovery>();
        }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public virtual ICollection<Discovery> PioneeringDiscoveries { get; set; }
        public virtual ICollection<Discovery> ObservationDiscoveries { get; set; }

    }
}
