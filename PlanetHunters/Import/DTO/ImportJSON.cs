using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetHunters.Import.DTO;
using PlanetHunters.Models;
using PlanetHunters;



namespace PlanetHunters.Import.JSON
{
    public class ImportJSON
    {
        public static void AddAstronomers(IEnumerable<AstronomerDTO> astronomerDtos)
        {
            Program.PhaseTyper("Importing Astronomers...");
            using (PlanetHuntersContext context = new PlanetHuntersContext())
            {
                foreach (AstronomerDTO astronomerDto in astronomerDtos)
                {
                    if (astronomerDto.FirstName == null || astronomerDto.LastName == null)
                    {
                        Program.PhaseTyper("Invalid Astronomer data");
                    }
                    else
                    {
                        Astronomer astronomer = new Astronomer
                        {
                            FirstName = astronomerDto.FirstName,
                            LastName = astronomerDto.LastName
                        };
                        context.Astronomers.Add(astronomer);
                        Program.PhaseTyper($"Astronomer {astronomer.FirstName} {astronomer.LastName} successfully imported");
                    }
                }
                context.SaveChanges();
            }

        }
        public static void AddTelescopes(IEnumerable<TelescopeDTO> telescopeDTOs)
        {
            Program.PhaseTyper("Importing Telescopes...");

            using (PlanetHuntersContext context = new PlanetHuntersContext())
            {
                foreach (TelescopeDTO telescopedto in telescopeDTOs)
                {


                    var telescopediamDouble = telescopedto.MirrorDiameter == null ? 0 : telescopedto.MirrorDiameter.Value;

                    if (telescopedto.Location == null || telescopedto.Name == null || telescopediamDouble <= 0.0)
                    {
                        Program.PhaseTyper("Invalid Telescope Data!");
                    }
                    else
                    {
                        Telescope telescope = new Telescope
                        {
                            Name = telescopedto.Name,
                            MirrorDiameter = telescopediamDouble,
                            Location = telescopedto.Location

                        };
                        context.Telescopes.Add(telescope);
                        Program.PhaseTyper($"{telescope.Name} added to database!");
                    }

                }
                context.SaveChanges();
            }

        }
        public static void AddStarSystems(IEnumerable<PlanetDTO> planetDTOs)
        {
            Program.PhaseTyper("Importing Star Systems...");

            using (PlanetHuntersContext context = new PlanetHuntersContext())
            {
                foreach (PlanetDTO planet in planetDTOs)
                {
                    if (planet.StarSystem != null && context.StarSystems.Where(ss => ss.Name == planet.StarSystem).ToList().Count == 0)
                    {
                        StarSystem ss = new StarSystem
                        {
                            Name = planet.StarSystem
                        };
                        context.StarSystems.Add(ss);
                        context.SaveChanges();
                        Program.PhaseTyper($"Star System {ss.Name} imported!");
                    }
                }

            }
        }
        public static void AddPlanets(IEnumerable<PlanetDTO> planetDTOs)
        {
            Program.PhaseTyper("Importing Planets...");

            using (PlanetHuntersContext context = new PlanetHuntersContext())
            {
                foreach (PlanetDTO planet in planetDTOs)
                {
                    if (planet.Mass <= 0)
                    {
                        Program.PhaseTyper("Planet Mass cannot be negative or zero! Invalid Planet!");
                    }
                    else
                    {
                       StarSystem ss = context.StarSystems.Where(s => s.Name == planet.StarSystem)
                     .FirstOrDefault();
                        Planet pl = new Planet
                        {
                            Name = planet.Name,
                            Mass = planet.Mass,
                            HostStarSystemId = ss.Id

                        };
                        context.Planets.Add(pl);
                        context.SaveChanges();
                        Program.PhaseTyper($"Planet {pl.Name} imported!");
                    }


                }
            }
        }
    }
}
