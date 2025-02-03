using PRG2_A2;

Terminal T5 = new Terminal("Terminal 5");

string[] als = File.ReadAllLines("airlines.csv");
string[] bgs = File.ReadAllLines("boardinggates.csv");

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
}
Console.WriteLine(string.Join(", ", T5.BoardingGates.Count));

Console.WriteLine(string.Join(", ", T5.BoardingGates.Keys));
Console.WriteLine(string.Join(", ", T5.BoardingGates.Values));


/*
1)	Load files (airlines and boarding gates)
	load the airlines.csv file - done
	create the Airline objects based on the data loaded -
	add the Airlines objects into an Airline Dictionary -
	load the boardinggates.csv file
	create the Boarding Gate objects based on the data loaded
	add the Boarding Gate objects into a Boarding Gate dictionary

2)	Load files (flights)
	load the flights.csv file
	create the Flight objects based on the data loaded
	add the Flight objects into a Dictionary

*/