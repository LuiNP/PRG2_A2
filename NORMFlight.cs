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
    internal class NORMFlight : Flight
    {

        public NORMFlight(string fN, string o, string d, DateTime eT, string s) : base(fN, o, d, eT, s) { }

        public override double CalculateFees()
        {
            double baseFee = 300;
            double arriveFee = 500;
            double departFee = 800;
            if (Destination == "Singapore (SIN)")
            {
                double fees = baseFee + arriveFee;//arriving fee is 500, base gate is 300, 500+300=800
                return fees;
            }
            else
            {
                double fees = baseFee + departFee;//if not arrivng, flight is departing. depart fee is 800, base gate is 300, 800+300=1100
                return fees;
            }
        }

        public override string ToString()
        {
            return "FlightNo: " + FlightNumber +
                "Origin: " + Origin +
                "Destination: " + Destination + 
                "ExpectedTime: " + ExpectedTime +
                "Status: " + Status;
        }
    }
}
