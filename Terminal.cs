using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_A2
{
    class Terminal
    {
        private string terminalName;
        private Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
        private Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
        private Dictionary<string, Flight> flights = new Dictionary<string, Flight>();
        private Dictionary<string, double> gateFees = new Dictionary<string, double>();

        public string TerminalName
        { 
            get { return terminalName; } 
            set {  terminalName = value; }
        }

        public Dictionary<string, Airline> Airlines
        {
            get { return airlines; }
        }
        public Dictionary<string, BoardingGate> BoardingGates
        {
            get { return boardingGates; }
        }
        public Dictionary<string, double> GateFees
        {
            get { return gateFees; }
        }

        public Dictionary<string, Flight> Flights
        {
            get { return flights; }
        }

        public bool AddAirline(Airline airline)
        {
            if (Airlines.ContainsKey(airline.Code)) //  check to avoid duplicates
            {
                Airlines.Add(airline.Code, airline);
                return true;
            }
            else
            {
                return false;//also make a note that it already exists
            }
        }
        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (BoardingGates.ContainsKey(boardingGate.GateName)) //  check to avoid duplicates
            {
                BoardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
            }
            else
            {
                return false;//also make a note that it already exists
            }
        }
        public Airline? GetAirlineFromFlight(Flight flight)
        {
            foreach (var airline in Airlines)
            {
                foreach (var flight_entry in airline.Value.Flights)
                {
                    if (flight == flight_entry.Value)
                    {
                        return airline.Value;
                    }
                }
            }
            return null;
        }

        public void PrintAirlineFees()
        {
            foreach (var airline in Airlines) 
            {
                Console.WriteLine("{0,0}{1,0}{3,0:F2}", airline.Key, ": $", airline.Value.CalculateFees());
            }
        }

        public override string ToString()
        {
            return "TerminalName: " + TerminalName +
                "Airlines: " + Airlines.Keys +
                "Flights: " + Flights.Keys +
                "BoardingGates: " + BoardingGates.Keys +
                "GateFees: " + GateFees.Values;
        }
    }
}
