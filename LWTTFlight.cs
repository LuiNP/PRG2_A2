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
    internal class LWTTFlight : Flight
    {
        private double requestFee = 300;
        public double RequestFee
        {
            get { return requestFee; }
            set { requestFee = value; }
        }
        public LWTTFlight(string fN, string o, string d, DateTime eT, string s) : base(fN, o, d, eT, s) { }

        public override double CalculateFees()
        {
            double baseFee = 300;
            double arriveFee = 500;
            double departFee = 800;
            if (Destination == "Singapore (SIN)")
            {
                double fees = baseFee + arriveFee + RequestFee;//arriving fee is 500, LWTT request fee is 500, base gate is 300, 500+500+300=1300
                return fees;
            }
            else
            {
                double fees = baseFee + departFee + RequestFee;//if not arrivng,. depart fee is 800,  LWTT request fee is 500, flight is departing, base gate is 300, 800+500+300=1600
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
