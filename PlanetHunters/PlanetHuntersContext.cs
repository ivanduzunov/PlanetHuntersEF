namespace PlanetHunters
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class PlanetHuntersContext : DbContext
    {
        public PlanetHuntersContext()
            : base("name=PlanetHuntersContext")
        {
        }

        public virtual DbSet<Astronomer> Astronomers { get; set; }
        public virtual DbSet<Discovery> Discoveries { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<StarSystem> StarSystems { get; set; }
        public virtual DbSet<Telescope> Telescopes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Discovery>()
                .HasMany(d => d.Pioneers)
                .WithMany(a => a.PioneeringDiscoveries)
                .Map(da =>
                {
                    da.ToTable("Pioneers");
                    da.MapLeftKey("DiscoveryId");
                    da.MapRightKey("AstronomerId");
                });
            modelBuilder.Entity<Discovery>()
    .HasMany(d => d.Observers)
    .WithMany(a => a.ObservationDiscoveries)
    .Map(da =>
    {
        da.ToTable("Observers");
        da.MapLeftKey("DiscoveryId");
        da.MapRightKey("AstronomerId");
    });


            modelBuilder.Entity<Telescope>().Property(t => t.MirrorDiameter).IsOptional();
            modelBuilder.Entity<Discovery>().HasRequired(d => d.Telescope).WithMany(t => t.Discoveries);
            modelBuilder.Entity<Planet>().HasRequired(p => p.HostStarSystem).WithMany(ss => ss.Planets);
            modelBuilder.Entity<Star>().HasRequired(p => p.HostStarSystem).WithMany(ss => ss.Stars);
            modelBuilder.Entity<Planet>().HasOptional(p => p.Discovery).WithMany(ss => ss.Planets);
            modelBuilder.Entity<Star>().HasOptional(p => p.Discovery).WithMany(ss => ss.Stars);


        }
    }
}