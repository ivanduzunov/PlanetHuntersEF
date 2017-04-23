using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PlanetHunters
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new PlanetHuntersContext();
            
           
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
    }
}
