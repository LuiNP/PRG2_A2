using PRG2_A2;
using System.Data;

Terminal T5 = new Terminal("Terminal 5");

string[] als = File.ReadAllLines("airlines.csv");
string[] bgs = File.ReadAllLines("boardinggates.csv");
string[] fs = File.ReadAllLines("flights.csv");

for (int i = 1; i < als.Length; i++)
{
    string[] aldetail = als[i].Split(',');
    Airline airline = new Airline(aldetail[0], aldetail[1]);
    T5.AddAirline(airline);
}

bool bgboolstrCheck(string s)
{
    if (s.Trim().ToLower() == "true")
    {
        return true;
    }
    else if (s.Trim().ToLower() == "false")
    {
        return false;
    }
    else
    {
        Console.WriteLine("Warning: Not recognised");
        return false;
        
    }

}



for (int i = 1; i < bgs.Length; i++)
{
    string[] bgdetail = bgs[i].Trim().Split(',');

    BoardingGate boardingGate = new BoardingGate(bgdetail[0], null, bgboolstrCheck(bgdetail[1]), bgboolstrCheck(bgdetail[2]), bgboolstrCheck(bgdetail[3]));
    //Console.WriteLine(bgdetail[0], null, bgboolstrCheck(bgdetail[1]), bgboolstrCheck(bgdetail[2]), bgboolstrCheck(bgdetail[3]));
    //for if You need to see the items manually
    T5.AddBoardingGate(boardingGate);
}

for (int i = 1; i < fs.Length; i++)
{
    string[] fdetail = fs[i].Split(',');//note: the fourth is either the code or a blank string. 0 is code, 1 is origin, 2 destination, 3 expected a/d, 4 for req
    if (fdetail[4].Trim() == "LWTT")
    {
        LWTTFlight flight = new LWTTFlight(fdetail[0], fdetail[1], fdetail[2], DateTime.Parse(fdetail[3]), "On Time");
        T5.Flights.Add(flight.FlightNumber, flight);
    }
    else if (fdetail[4].Trim() == "DDJB")
    {
        DDJBFlight flight = new DDJBFlight(fdetail[0], fdetail[1], fdetail[2], DateTime.Parse(fdetail[3]), "On Time");
        T5.Flights.Add(flight.FlightNumber, flight);
    }
    else if (fdetail[4].Trim() == "CFFT")
    {
        CFFTFlight flight = new CFFTFlight(fdetail[0], fdetail[1], fdetail[2], DateTime.Parse(fdetail[3]), "On Time");
        T5.Flights.Add(flight.FlightNumber, flight);
    }
    else if (fdetail[4].Trim() == "")
    {
        NORMFlight flight = new NORMFlight(fdetail[0], fdetail[1], fdetail[2], DateTime.Parse(fdetail[3]), "On Time");//gonna need to make sure any date inputed is correct
        T5.Flights.Add(flight.FlightNumber, flight);
    }
    else { Console.WriteLine("{0}{1}", "Unrecognised request code in item ", i - 1); }
}


foreach (var flight in T5.Flights)
{
    bool found = false;//upon airline of flight being found, set to true
    var f = flight.Value;// The value (flight object of type class)
    string fa = f.FlightNumber.Split(' ')[0];//the airline code
    foreach(var airline in T5.Airlines)
    {
        if (fa.Contains(airline.Key))
        {
            airline.Value.AddFlight(flight.Value);
            found = true;
        }
    }
    if (found == false)
    {
        Console.WriteLine("Flight Airline not recognised");//airline not in terminal dict
    }
}



/*foreach (var airlineEntry in T5.Airlines)//displays the flights in the airline.flights dict
{
    var airline = airlineEntry.Value;
    Console.WriteLine(airline.Flights.Count());
    foreach (var flightEntry in airline.Flights)
    {
        var flight = flightEntry.Value; // Access the Flight object
        Console.WriteLine($"      Flight Number: {flight.FlightNumber}");
    }
}
*/

int numOptionCheck(int hi, int lo, string chooseMsg)
{
    Console.Write(chooseMsg);
    var input = Console.ReadLine();
    try
    {
        int option = Convert.ToInt32(input);
        if (option < hi && option > lo)
        {
            return option;
        }
        else
        {
            Console.WriteLine("Please input valid option");
            return -1;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Please input a number");
        return -1;
    }
    catch
    {
        Console.WriteLine("Unknown error");
        return -1;
    }

}

int Menu()//need to adjust format
{
    Console.WriteLine("---------------------------------------");
    Console.WriteLine("Terminal 5 Menu");
    Console.WriteLine("[1} List all flights with their basic information");
    Console.WriteLine("[2} List all boarding gates");
    Console.WriteLine("[3] Assign a boarding gate to a flight");
    Console.WriteLine("[4] Create a new flight");
    Console.WriteLine("[5] Display full flight details from an airline");
    Console.WriteLine("[0] Exit");
    Console.WriteLine("---------------------------------------");
    while (true)
    {
        return numOptionCheck(6, -1, "Choose Option: ");
    }
}
void ListAllFlights()
{
    Console.WriteLine();
    Console.WriteLine("{0,-15}{1,-20}{2,-20}{3}", "Flight Number", "Origin", "Destination", "Expected Time");
    foreach (var flight in T5.Flights)
    {
        Flight flightentry = flight.Value;
        Console.WriteLine("{0,-15}{1,-20}{2,-20}{3,6}", flightentry.FlightNumber, flightentry.Origin, flightentry.Destination, flightentry.ExpectedTime);
    }
}

void ListAllBoardingGates()
{
    Console.WriteLine("{0,-15}{1,-10}{2,-10}{3,-10}{4}", "Gate Name", "DDJB", "CFFT", "LWTT", "Assigned Flight");
    foreach (var gate in T5.BoardingGates)
    {
        BoardingGate gateentry = gate.Value;
        Console.WriteLine("{0,-15}{1,-10}{2,-10}{3,-10}{4}", gateentry.GateName, gateentry.SupportsDDJB, gateentry.SupportsCFFT, gateentry.SupportsLWTT, gateentry.Flight?.FlightNumber);
    }
}

void AssignBoardingGate()
{
    bool flightFound = false;

    while (!flightFound)
    {
        Console.Write("Flight number: ");
        string input = Console.ReadLine();

        foreach (var flight in T5.Flights)
        {
            Flight f = flight.Value;
            if (input == f.FlightNumber)
            {
                Flight flightentry = flight.Value;
                Console.WriteLine("{0}{1}", "Flight Number: ", flightentry.FlightNumber);
                Console.WriteLine("{0}{1}", "Origin: ", flightentry.Origin);
                Console.WriteLine("{0}{1}", "Destination: ", flightentry.Destination);
                Console.WriteLine("{0}{1}", "Expected Time: ", flightentry.ExpectedTime);

                if (flightentry is NORMFlight)
                {
                    Console.WriteLine("Special request code: None");
                }
                else if (flightentry is DDJBFlight)
                {
                    Console.WriteLine("Special request code: DDJB");
                }
                else if (flightentry is CFFTFlight)
                {
                    Console.WriteLine("Special request code: CFFT");
                }
                else if (flightentry is LWTTFlight)
                {
                    Console.WriteLine("Special request code: LWTT");
                }

                flightFound = true;


                bool gateFound = false;
                while (!gateFound)
                {
                    Console.Write("Gate number: ");
                    string gateput = Console.ReadLine();

                    foreach (var gate in T5.BoardingGates)
                    {
                        BoardingGate g = gate.Value;
                        if (gateput == g.GateName)
                        {
                            gateFound = true;
                            if (g.Flight is null)
                            {
                                Console.WriteLine("{0}{1}", "Supports DDJB", g.SupportsDDJB);
                                Console.WriteLine("{0}{1}", "Supports CFFT", g.SupportsCFFT);
                                Console.WriteLine("{0}{1}", "Supports LWTT", g.SupportsLWTT);
                                while (true)
                                {
                                    Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
                                    string update = Console.ReadLine();
                                    if (update == "Y")
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine("1. Delayed");
                                            Console.WriteLine("2. Boarding");
                                            Console.WriteLine("3. On Time");
                                            int stat = numOptionCheck(4, 0, "Please select the new status of the flight: ");
                                            {
                                                if (stat == 1)
                                                {
                                                    flightentry.Status = "Delayed";
                                                    break;
                                                }
                                                else if (stat == 2)
                                                {
                                                    flightentry.Status = "Boarding";
                                                    break;
                                                }
                                                else
                                                {
                                                    flightentry.Status = "On Time";
                                                    break;
                                                }
                                            }
                                        }break;

                                    }
                                    else if (update == "N")
                                    {
                                        Console.WriteLine("{0}{1}{2}{3}{4}", "Flight ", input, " has been assigned to Boarding Gate ", gateput, "!");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please select a valid option");
                                    }
                                    }
                            g.Flight = flightentry;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Gate already assigned");
                            }

                        }
                    }

                    if (!gateFound)
                    {
                        Console.WriteLine("Gate not found, please try again.");
                    }
                }
            }
        }
        if (!flightFound)
        {
            Console.WriteLine("Flight not found, please try again.");
        }
    }


}

void CreateFlight()
{
    string fIn;
    string dIn;
    string codeIn;   


    while (true)//for if you want to add multiple
    {
        while (true)//set up flight number
        {
            Console.Write("Enter Flight Number: ");
            string fn = Console.ReadLine();
            if (fn is null)
                {
                Console.WriteLine("Please input");
                }
            else if (fn.Length != 6)
                {
                Console.WriteLine("Please input valid flight number(AB 123)");
                }
            bool unique = true;
            foreach (var f in T5.Flights)
            {
                Flight fEntry = f.Value;
                if (fn == fEntry.FlightNumber)
                {
                    Console.WriteLine("Flight Number already exists");
                    unique = false;
                }
            }
            if (unique == true)
            {
                fIn = fn;
                break;
            }
        }
        Console.Write("Enter Origin: ");
        string o = Console.ReadLine();
        Console.Write("Enter Destination: ");
        string d = Console.ReadLine();
        while (true)
        {
            Console.Write("Enter Expected Departure/Arrical Time (dd/mm/yyyy hh:mm): ");
            try
            {
                string datstring = Console.ReadLine();
                DateTime dat = DateTime.Parse(datstring);
                dIn = datstring;
                break;
            }
            catch (FormatException ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        while(true)
        {
            try
            {
                bool valcode = false;
                Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ") ;
                string code = Console.ReadLine();
                string[] valids = ["CFFT", "DDJB", "LWTT", "NONE"];
                for (int i = 0; i < valids.Length; i++)
                {
                    if (code.ToUpper() == valids[i])
                        { 
                        codeIn = code;
                        valcode = true;
                        }
                }
                if (valcode == true) { break; }
                else { Console.WriteLine("Invalid code"); } ;
            }
            catch(Exception ex) { Console.WriteLine(ex.Message);}
            
        }
        Console.WriteLine("{0}{1}{2}", "Flight ", fIn, " has been added!") ;
        Console.WriteLine("Would you like to add another flight ? (Y/N)");
        string yn = Console.ReadLine();
        try
        {
            if (yn.ToUpper() == "N")
            {
                break;
            }
            else if(yn.ToUpper() != "Y")
            {
                Console.WriteLine("Please choose");
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
        




    }
}

void DisplayAirlineDetails()
{
    Console.WriteLine("{0,-15}{1}", "Airline Code", "Airline Name");
    foreach (var airline in T5.Airlines)
    {
        Airline alentry = airline.Value;
        Console.WriteLine("{0,-15}{1}", alentry.Code, alentry.Name);
    }
    bool codeFound = false;
    string showcode;
    while (true)
    {
        Console.Write("Enter Airline Code: ");
        string selcode = Console.ReadLine();
        foreach (var airline in T5.Airlines)
        {
            Airline alcode = airline.Value;
            if (selcode.ToUpper() == alcode.Code) { codeFound = true; break; }
        }
        if (codeFound == true) { showcode = selcode;  break; }
        else { Console.WriteLine("Code not found"); }
    }
    Console.WriteLine("{0}{1}", "List of flights from ", T5.Airlines[showcode].Name);
    Console.WriteLine("{0,-15}{1,-25}{2,-25}{3,-25}{4,-25}{5}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure / Arrival Time");
    foreach (var flight in T5.Airlines[showcode].Flights)
    {
        Flight thisflight = flight.Value;
        Console.WriteLine("{0,-15}{1,-25}{2,-25}{3,-25}{4,-25}{5}", thisflight.FlightNumber, T5.Airlines[showcode].Name, thisflight.Origin, thisflight.Destination, thisflight.ExpectedTime);
    }
    
}


while (true)
    {
        int option = Menu();
        if (option == 1)
        {
            ListAllFlights();
        }
        else if (option == 2)
        {
            ListAllBoardingGates();
        }
        else if (option == 3)
        {
            AssignBoardingGate();
        }
        else if (option == 4)
        {
            CreateFlight();
        }
        else if (option == 5)
        {
        DisplayAirlineDetails();
        }
        else if (option == 0)
        {
            break;
        }

    }
