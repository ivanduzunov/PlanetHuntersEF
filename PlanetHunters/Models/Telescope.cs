using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PlanetHunters.Models
{
    public class Telescope
    {
        public Telescope()
        {
            this.Discoveries = new HashSet<Discovery>();
        }
        private double mirorDiameter;
        private decimal mirrorDiameter { get; set; }
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string Location { get; set; }
        public double MirrorDiameter
        {
            get
            {
                return mirorDiameter;
            }
            set
            {
                double checkDiam = value;
                if (checkDiam > 0.0)
                {
                    this.mirorDiameter = value;
                }
                else
                {
                    throw new Exception("Invalid Miror Diameter!");
                }
            }
        }
        public virtual ICollection<Discovery> Discoveries { get; set; }
    }
}
