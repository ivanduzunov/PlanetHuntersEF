using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using PlanetHunters.Import.JSON;
using System.IO;
using PlanetHunters.Import.DTO;
using System.Xml.Linq;


namespace PlanetHunters
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new PlanetHuntersContext();
            InitDB(context);
            ImportAstronomers(context);
            ImportTelescopes(context);
            ImportStarSystemsAndPlanets(context);
            ImportStars(context);
            ImportDiscoveries(context);
        }
        public static void InitDB(PlanetHuntersContext cnxt)
        {
            cnxt.Database.Initialize(true);
            PhaseTyper("Congratulations! Database Initialized successfully!");
        }
        public static void PhaseTyper(string text)
        {

            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
        }
        public static void ImportAstronomers(PlanetHuntersContext context)
        {
            PhaseTyper("Reading file Astronomers.json");
            var json = File.ReadAllText(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\PlanetHunters\PlanetHunters\Import\JSON\astronomers.json");

            var astronomersDTOsList = JsonConvert.DeserializeObject<IEnumerable<AstronomerDTO>>(json);
            ImportJSON.AddAstronomers(astronomersDTOsList);


        }
        public static void ImportTelescopes(PlanetHuntersContext context)
        {
            PhaseTyper("Reading telescopes.json");
            var json = File.ReadAllText(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\PlanetHunters\PlanetHunters\Import\JSON\telescopes.json");
            var List = JsonConvert.DeserializeObject<IEnumerable<TelescopeDTO>>(json);
            ImportJSON.AddTelescopes(List);
            PhaseTyper("Telescopes added to database succesfully");
        }
        public static void ImportStarSystemsAndPlanets(PlanetHuntersContext context)
        {
            PhaseTyper("Reading planets.json");
            var json = File.ReadAllText(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\PlanetHunters\PlanetHunters\Import\JSON\planets.json");
            var List = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);
            ImportJSON.AddStarSystems(List);
            PhaseTyper("All Star Systems Imported!");
            ImportJSON.AddPlanets(List);
            PhaseTyper("All Planets Imported!");

        }
        public static void ImportStars(PlanetHuntersContext context)
        {
            PhaseTyper("Reading stars.xml");
            XDocument starsDocument = XDocument.Load(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\PlanetHunters\PlanetHunters\Import\XML\stars.xml");
            XElement root = starsDocument.Root;
            ImportXML.AddStars(root);
        }
        public static void ImportDiscoveries(PlanetHuntersContext context)
        {
            PhaseTyper("Reading discoveries.xml");
            XDocument discoveriesDocument = XDocument.Load(@"C:\Users\Mihail\Documents\visual studio 2015\Projects\PlanetHunters\PlanetHunters\Import\XML\discoveries.xml");
            XElement root = discoveriesDocument.Root;
            ImportXML.AddDiscoveriesDTO(root);
        }
    }

}
