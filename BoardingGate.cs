﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_A2
{
    class BoardingGate
    {
        private string gateName;

        private bool supportsCFFT;

        private bool supportsDDJB;

        private bool supportsLWTT;

        private Flight? flight;
        public string GateName
        {
            get { return gateName; }
            set { gateName = value; }
        }
        public bool SupportsCFFT
                {
                    get { return supportsCFFT; }
                    set { supportsCFFT = value; }
                }

        public bool SupportsDDJB
        { 
          get { return supportsDDJB; }
          set { supportsDDJB = value; }
        }

        public bool SupportsLWTT
        { 
            get { return supportsLWTT; }
            set { supportsLWTT = value; }
        }

        public Flight Flight
        { 
            get { return flight; } 
            set {  flight = value; } 
        }

        public BoardingGate(string gN, Flight f)
        {
            GateName = gN;
            Flight = f;
        }

        public double CalculateFees()
        {
            return Flight.CalculateFees();
        }

        public override string ToString()
        {
            return "Name: " + GateName +
                "Supports CFFT: " + supportsCFFT +
                "Supports DDJB: " + supportsDDJB +
                "Supports LWTT" + supportsLWTT +
                "Flight: " + Flight.FlightNumber;
        }
    }
}
