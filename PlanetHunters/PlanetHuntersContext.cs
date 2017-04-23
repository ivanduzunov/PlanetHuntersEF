namespace PlanetHunters
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PlanetHuntersContext : DbContext
    {
        public PlanetHuntersContext()
            : base("name=PlanetHuntersContext")
        {
        }


        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}