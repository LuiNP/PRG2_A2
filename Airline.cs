using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_A2
{
    class Airline
    {
        private string name;
        private string code;
        private Dictionary<string, Flight>flights = new Dictionary<string, Flight>();

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public Dictionary<string, Flight>Flights
        {
            get { return flights; }
        }

        public Airline(string n, string c) 
        {
            name = n;
            code = c;
        }

        public bool AddFlight(Flight flight)
        {
            if (!Flights.ContainsKey(flight.FlightNumber)) //  check to avoid duplicates
            {
                Flights.Add(flight.FlightNumber, flight);
                return true;
            }
            else
            {
                return false;//also make a note that it already exists
            }
        }

        public bool RemoveFlight(string flightCode)
        {
            return Flights.Remove(flightCode);  // Returns true if the flight was removed, false if not found
        }
        public double CalculateFees()
        {
            double fees = 0;
            foreach (var entry in Flights) 
            {
                fees += entry.Value.CalculateFees();
            }
            return fees;
        }

        public override string ToString()
        {
            return "Name: " + Name +
                "Code; " + Code +
                "Flights: " + Flights.Count;
        }
    }
}
