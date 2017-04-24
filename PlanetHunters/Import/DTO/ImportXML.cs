using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetHunters.Import.DTO;
using System.Xml.Linq;
using PlanetHunters.Models;
using PlanetHunters;
using System.Xml;

namespace PlanetHunters.Import.DTO
{
    public class ImportXML
    {
        public static void AddStars(XElement root)
        {
            Program.PhaseTyper("Importing Stars Data!");

            using (PlanetHuntersContext context = new PlanetHuntersContext())
            {
                foreach (XElement element in root.Elements())
                {
                    StarDTO starDto = new StarDTO
                    {
                        Name = element.Element("Name").Value,
                        Temperature = int.Parse(element.Element("Temperature").Value),
                        StarSystem = element.Element("StarSystem").Value
                    };
                    if (starDto.Name == null || starDto.StarSystem == null || starDto.Temperature < 2400)
                    {
                        Program.PhaseTyper("Invalid Star Data!");
                    }
                    else
                    {
                        if (context.StarSystems.Where(stars => stars.Name == starDto.StarSystem).ToList().Count() == 0)
                        {
                            StarSystem s = new StarSystem
                            {
                                Name = starDto.StarSystem
                            };
                            context.StarSystems.Add(s);
                            context.SaveChanges();
                            Program.PhaseTyper($"New star system - {s.Name} added to DB!");
                        }
                        StarSystem ss = context.StarSystems.Where(ssystem => ssystem.Name == starDto.StarSystem).FirstOrDefault();
                        Star star = new Star
                        {
                            Name = starDto.Name,
                            Temperature = starDto.Temperature,
                            HostStarSystemId = ss.Id
                        };
                        context.Stars.Add(star);
                        context.SaveChanges();
                        Program.PhaseTyper($"Star {star.Name} added to DB!");
                    }

                }

            }
        }
        public static void AddDiscoveriesDTO(XElement root)
        {
            Program.PhaseTyper("Importing Discoveries Data!");
            using (PlanetHuntersContext context = new PlanetHuntersContext())
            {

                var discoveries = root.Elements().Select(d => new DiscoveryDTO
                {
                    DateMade = DateTime.Parse(d.Attribute("DateMade").Value),
                    Telescope = d.Attribute("Telescope").Value,
                    Pioneers = d.Element("Pioneers").Elements().Select(p => p.Value).ToList(),
                    Observers = d.Element("Observers").Elements().Select(o => o.Value).ToList(),
                    Stars = d.Element("Stars").Elements().Select(s => s.Value).ToList(),
                    Planets = d.Element("Planets").Elements().Select(p => p.Value).ToList(),
                }).ToList();

                AddDiscoveries(discoveries);



            }

        }
        private static void AddDiscoveries(List<DiscoveryDTO> discoveries)
        {
            using (PlanetHuntersContext context = new PlanetHuntersContext())
            {
                foreach (DiscoveryDTO discoveryDto in discoveries)
                {

                    if ((discoveryDto.Stars.Any() || discoveryDto.Planets.Count == 0 ||
                       discoveryDto.Pioneers.Count == 0 || discoveryDto.Observers.Count == 0)
                        || discoveryDto.DateMade == null || discoveryDto.Telescope == null)
                    {
                        Program.PhaseTyper("Invalid Discovery!");
                    }
                    else
                    {
                        var planets = discoveryDto.Stars.Select(s => context.Planets.FirstOrDefault(ss => ss.Name == s)).ToList();
                        var stars = discoveryDto.Stars.Select(s => context.Stars.FirstOrDefault(ss => ss.Name == s)).ToList();
                        var pioneers = discoveryDto.Stars.Select(s => context.Astronomers.
                        FirstOrDefault(ss => ss.FirstName + " " + ss.LastName == s)).ToList();
                        var observers = discoveryDto.Stars.Select(s => context.Astronomers.
                        FirstOrDefault(ss => ss.FirstName + " " + ss.LastName == s)).ToList();

                        Discovery discovery = new Discovery
                        {
                            DateMade = discoveryDto.DateMade,
                            TelescopeId = context.Telescopes.Where(t => t.Name == discoveryDto.Telescope).Select(t => t.Id).First(),
                            Planets = planets,
                            Stars = stars,
                            Observers = observers,
                            Pioneers = pioneers
                        };
                        context.Discoveries.Add(discovery);
                        context.SaveChanges();
                        Program.PhaseTyper($"Discovery, that is made on {discovery.DateMade} is imported in the DB!");

                    }
                }
            }
        }
    }
}

