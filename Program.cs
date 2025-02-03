﻿using PRG2_A2;

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
    if (s == "True")
    {
        return true;
    }
    else if (s == "False")
    {
        return false;
    }
    else
    {
        return false;
        Console.WriteLine("Warning: Not recognised");
    }

}


for (int i = 1; i < bgs.Length; i++)
{
    string[] bgdetail = bgs[i].Split(',');

    BoardingGate boardingGate = new BoardingGate(bgdetail[0], null, bgboolstrCheck(bgdetail[1]), bgboolstrCheck(bgdetail[2]), bgboolstrCheck(bgdetail[3]));
    T5.AddBoardingGate(boardingGate);


    for (int i2 = 1; i < fs.Length; i++)
    {
        string[] fdetail = fs[i].Split(',');//note: the fourth is either the code or a blank string. 0 is code, 1 is origin, 2 destination, 3 expected a/d, 4 for req
        if (fdetail[4] == "LWTT")
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
        else { Console.WriteLine("{0}{1}", "Unrecognised request code in item ", i2 - 1); }
    }
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

/*
1)	Load files (airlines and boarding gates)
	load the airlines.csv file - done
	create the Airline objects based on the data loaded -
	add the Airlines objects into an Airline Dictionary -
	load the boardinggates.csv file-
	create the Boarding Gate objects based on the data loaded-
	add the Boarding Gate objects into a Boarding Gate dictionary-

2)	Load files (flights)
	load the flights.csv file
	create the Flight objects based on the data loaded
	add the Flight objects into a Dictionary

3)	List all flights with their basic information
	display the Basic Information of all Flights, which are the 5 flight specifications (i.e. Flight Number, Airline Name, Origin, Destination, and Expected Departure/Arrival Time)

4)	List all boarding gates
	display all the Boarding Gates in Terminal 5 with all of the Special Request Codes they service (if any) and Flight Numbers assigned (if any)

5)	Assign a boarding gate to a flight
	prompt the user for the Flight Number
	display the basic information of the selected Flight, including the Special Request Code (if any)
	prompt the user for the Boarding Gate
	check that the selected Boarding Gate is not assigned to another Flight (Note: For Basic Features, there is no need to validate if the Special Request Codes between Flights and Boarding Gates match)
o	if the Boarding Gate selected is already assigned to another flight, display a message that the Boarding Gate is already assigned and repeat the previous step
	display the basic information of the selected Flight, Special Request Code (if any), and Boarding Gate entered
	prompt the user if they would like to update the Status of the Flight, with a new Status of any of the following options: “Delayed”, “Boarding”, or “On Time” [Y] or set the Status of the Flight to the default of “On Time” and continue to the next step if [N]
	display a message to indicate a successful Boarding Gate assignment

6)	Create a new flight
	prompt the user to enter the new Flight, which minimally requires the 4 flight specifications (i.e. Flight Number, Origin, Destination, and Expected Departure/Arrival Time)
	prompt the user if they would like to enter any additional information, like the Special Request Code
	create the proper Flight object with the information given
	add the Flight object to the Dictionary
	append the new Flight information to the flights.csv file
	prompt the user asking if they would like to add another Flight, repeating the previous 5 steps if [Y] or continuing to the next step if [N]
	display a message to indicate that the Flight(s) have been successfully added

7)	Display full flight details from an airline
	list all the Airlines available
	prompt the user to enter the 2-Letter Airline Code (e.g. SQ or MH, etc.)
	retrieve the Airline object selected
	for each Flight from that Airline, show their Airline Number, Origin and Destination
	prompt the user to select a Flight Number
	retrieve the Flight object selected
	display the following Flight details, which are all the flight specifications (i.e. Flight Number, Airline Name, Origin, Destination, and Expected Departure/Arrival Time, Special Request Code (if any) and Boarding Gate (if any))

	Validations (and feedback)
	The program should handle all invalid entries by the user, including empty responses and unexpected input datatypes (e.g. invalid 2-Letter Airline Code, invalid Flight Number, invalid Boarding Gate, etc.)
	If the user makes a mistake in the entry, the program should inform the user via appropriate feedback

When implementing the Advanced Features below, you are free to add onto but NOT MODIFY the Class Diagram where appropriate to complete them (i.e. changing the Class Diagram for Basic Features is NOT ALLOWED).

(a)	Process all unassigned flights to boarding gates in bulk
	for each Flight, check if a Boarding Gate is assigned; if there is none, add it to a queue
	display the total number of Flights that do not have any Boarding Gate assigned yet
	for each Boarding Gate, check if a Flight Number has been assigned
	display the total number of Boarding Gates that do not have a Flight Number assigned yet
	for each Flight in the queue, dequeue the first Flight in the queue
o	check if the Flight has a Special Request Code
	if yes, search for an unassigned Boarding Gate that matches the Special Request Code
	if no, search for an unassigned Boarding Gate that has no Special Request Code
o	assign the Boarding Gate to the Flight Number
o	display the Flight details with Basic Information of all Flights, which are the 5 flight specifications (i.e. Flight Number, Airline Name, Origin, Destination, and Expected Departure/Arrival Time), Special Request Code (if any) and Boarding Gate
	display the total number of Flights and Boarding Gates processed and assigned
	display the total number of Flights and Boarding Gates that were processed automatically over those that were already assigned as a percentage

(b)	Display the total fee per airline for the day
	check that all Flights have been assigned Boarding Gates; if there are Flights that have not been assigned, display a message for the user to ensure that all unassigned Flights have their Boarding Gates assigned before running this feature again
	for each Airline, retrieve all their Flights
o	for each Flight
	check if the Origin or Destination is Singapore (SIN), and apply the respective fee of $800 or $500 accordingly
	check if the Flight has indicated a Special Request Code and charge the appropriately listed Additional Fee
	apply the Boarding Gate Base Fee of $300
	compute the subtotal of fees to be charged for each Airline for the day
	compute the subtotal of discounts to be applied for each Airline based on the Promotional Conditions that they qualify for
	display the total final fees to be charged with a breakdown of the original subtotal calculated against the subtotal of discounts for the day
	compute and display the subtotal of all the Airline fees to be charged, the subtotal of all Airline discounts to be deducted, the final total of Airline fees that Terminal 5 will collect, and the percentage of the subtotal discounts over the final total of fees


*/