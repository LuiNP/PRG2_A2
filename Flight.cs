using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10258280G
// Student Name	: Reyes Luis Raphael Penaredondo
// (Luis)
//==========================================================

namespace PRG2_A2
{
    abstract class Flight
    {
        private string flightNumber;
        private string origin;
        private string destination;
        private DateTime expectedTime;
        private string status;


        public string FlightNumber
        {
            get { return flightNumber; }
            set {  flightNumber = value; }
        }

        public string Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public DateTime ExpectedTime
        {
            get { return expectedTime; }
            set { expectedTime = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public Flight(string fN, string o, string d, DateTime eT, string s)
        {
            FlightNumber = fN;
            Origin = o;
            Destination = d;
            ExpectedTime = eT;
            Status = s;
        }

        public abstract double CalculateFees();
        public override abstract string ToString();
    }
}
