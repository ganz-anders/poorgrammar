class AvalancheStatusReport
{
    private List<AvalancheLevel_Height> AvalancheLevel_ac2Height;               //field for the Avalanche Level(s) in dependent of the height
    private Dictionary<Direction, List<SnowProblem>> SnowProblem_Direction;     //field for the Snow Problems in dependent of the direction
    public void printReport(string filepath)        //printing all information from the  AvalancheStatusReport to the a Stream of overhanded filepath
    {
        StreamWriter sw = new StreamWriter(filepath, append:true);
        sw.WriteLine($"Lawinen-Lage-Bericht für heute den {DateTime.Today} \n...");
        foreach (var item in AvalancheLevel_ac2Height)
        {
            sw.WriteLine($"Lawinenlagestufe bis {item.UpperLimit}m: {Enum.GetName(typeof(AvalancheLevel), item.AvalancheLevel)}");
        }

        sw.WriteLine("Hauptschneeprobleme nach Himmelsrichtung:");
        List<string> output = new List<string>();
        int n;
        string? myEnumName;
        for (int i = 0; i < 8; i++)
        {
            sw.Write($"{Enum.GetName(typeof(Direction), i)}: ");
            output.Clear();
            n=0;
            foreach (SnowProblem item in SnowProblem_Direction[(Direction)i])
            {
                myEnumName=Enum.GetName(item);
                if (myEnumName!=null)
                {
                    output.Add((string)myEnumName);
                }
                n++;
            }
            sw.WriteLine(string.Join(',',output));
        }
        Console.WriteLine("_____");
        sw.Close();

    }

    public void printReport()       //printing all information from the  AvalancheStatusReport to the Console
    {
        Console.WriteLine($"Lawinen-Lage-Bericht für heute den {DateTime.Today} \n...");
        foreach (var item in AvalancheLevel_ac2Height)
        {
            Console.WriteLine($"Lawinenlagestufe bis {item.UpperLimit}m: {Enum.GetName(typeof(AvalancheLevel), item.AvalancheLevel)}");
        }

        Console.WriteLine("Hauptschneeprobleme nach Himmelsrichtung:");
        List<string> output = new List<string>();
        int n;
        string? myEnumName;
        for (int i = 0; i < 8; i++)
        {
            Console.Write($"{Enum.GetName(typeof(Direction), i)}: ");
            output.Clear();
            n=0;
            foreach (SnowProblem item in SnowProblem_Direction[(Direction)i])
            {
                myEnumName=Enum.GetName(item);
                if (myEnumName!=null)
                {
                    output.Add((string)myEnumName);
                }
                n++;
            }
            Console.WriteLine(string.Join(',',output));
        }
        Console.WriteLine("_____");
    }

    public AvalancheLevel getAvalancheLevel_Height(int height)
    {
        foreach (var item in AvalancheLevel_ac2Height)
        {
            if (item.UpperLimit>height) //checks if height is in current altitude level
            {
                return item.AvalancheLevel;
            }
        }

        throw new Exception("Error! AvalanchLevel_acording2Height seams to be damaged. altitude not found.");
    }

    public List<SnowProblem> getSnowProblem_Direction(Direction direction)  //return the list of SnowProblems from the dictionary entry for the overhanded direction
    {
        return SnowProblem_Direction[direction];
    }

    public AvalancheStatusReport()          //Constructor loading the Avalanche Report from user inputs from the Command Line
    {
        const int totalUpperLimit=9000; //9000m is the upperLimit for the last, open to the top, Avalanche level, , bc. no mountain is higher
        bool subdiv=false;              //is true, if there is a subdivision in more diferent Avalanche levels in dependence of the height
        bool isinput=false;             //is set true, if there was a *correct* user input
        string? input;                  //variable for the input
        int upperLimit;
        AvalancheLevel? AVLevel=null;
        SnowProblem_Direction=new Dictionary<Direction, List<SnowProblem>>();
        AvalancheLevel_ac2Height=new List<AvalancheLevel_Height>();

        Console.WriteLine($"Bitte geben Sie den Lawinen-Lage-Bericht für heute den {DateTime.Now} ein");
        Console.WriteLine("...");

        //read in the Avalanche Levels dependet on height
        Console.WriteLine("Gibt es eine Unterteilung des LLB in verschiedene Höhenstufen? [y/n]?"); //questioning subdivision
        input=Console.ReadLine();
        isinput=false;
        do
        {
            switch (input)
            {
                case "y":
                    isinput=true;
                    subdiv=true;
                    break;
                case "n":
                    isinput=true;
                    subdiv=false;
                    break;
                case "Y":
                    isinput=true;
                    subdiv=true;
                    break;
                case "N":
                    isinput=true;
                    subdiv=false;
                    break;
                default:
                    Console.WriteLine("Eingabe nicht erkannt.\nGibt es eine Unterteilung des LLB in verschiedene Höhenstufen? [y/n]?");
                    input=Console.ReadLine();
                    break;
            }   
        } while (!isinput);

        //while there are one ore more subdivisions
        while (subdiv)
        {
            //read in upper Limit
            Console.WriteLine("Geben Sie die Grenze für die unterste Unterteilung ein.");
            input=Console.ReadLine();
            while (!(int.TryParse(input,out upperLimit)))
            {
                Console.WriteLine("Eingabe nicht erkannt.\nGeben Sie die Grenze für die unterste Unterteilung ein.");
                input=Console.ReadLine();           
            }
            
            //read in associated AVLevel
            Console.WriteLine("Geben Sie die zugehörige Lagestufe an.");
            Console.WriteLine("1-gering, 2-mäßig, 3-erheblich, 4-groß, 5-sehr groß");
            AVLevel=null;
            input=Console.ReadLine();
            while (AVLevel==null)
            {
                switch (input)
                {
                    case "1":
                        AVLevel=AvalancheLevel.gering;
                        break;
                    case "2":
                        AVLevel=AvalancheLevel.mäßig;
                        break;
                    case "3":
                        AVLevel=AvalancheLevel.erheblich;
                        break;
                    case "4":
                        AVLevel=AvalancheLevel.groß;
                        break;
                    case "5":
                        AVLevel=AvalancheLevel.sehr_groß;
                        break;
                    default:
                        Console.WriteLine("Eingabe nicht erkannt. Geben Sie die zugehörige Lagestufe an.");
                        input=Console.ReadLine();
                        break;
                }
            }

            //add AVLevel to List
            AvalancheLevel_ac2Height.Add(new AvalancheLevel_Height(upperLimit,(AvalancheLevel)AVLevel));

            //questioning if there are more subdivs
            Console.WriteLine("Gibt es weitere Unterteilungen? [y/n]");
            isinput=false;
            input=Console.ReadLine();
            do
            {
                switch (input)
                {
                    case "y":
                        isinput=true;
                        subdiv=false;
                        break;
                    case "n":
                        isinput=true;
                        subdiv=false;
                        break;
                    case "Y":
                        isinput=true;
                        subdiv=true;
                        break;
                    case "N":
                        isinput=true;
                        subdiv=false;
                        break;
                    default:
                        Console.WriteLine("Eingabe nicht erkannt.\nGibt es weitere Unterteilungen? [y/n]");
                        input=Console.ReadLine();
                        break;
                }   
            } while (!isinput);

        }
        
        //read in the last (most high up) AVLevel, when all subdivs are processed
        Console.WriteLine("Geben Sie die Lagestufe an.");
        Console.WriteLine("1-gering, 2-mäßig, 3-erheblich, 4-groß, 5-sehr groß");
        input=Console.ReadLine();
        AVLevel=null;
        while (AVLevel==null)
        {
            switch (input)
            {
                case "1":
                    AVLevel=AvalancheLevel.gering;
                    break;
                case "2":
                    AVLevel=AvalancheLevel.mäßig;
                    break;
                case "3":
                    AVLevel=AvalancheLevel.erheblich;
                    break;
                case "4":
                    AVLevel=AvalancheLevel.groß;
                    break;
                case "5":
                    AVLevel=AvalancheLevel.sehr_groß;
                    break;
                default:
                    Console.WriteLine("Eingabe nicht erkannt. Geben Sie die Lagestufe an.");
                    input=Console.ReadLine();
                    break;
            }
        }
        AvalancheLevel_ac2Height.Add(new AvalancheLevel_Height(totalUpperLimit,(AvalancheLevel)AVLevel)); //add the last AVLevel


        //read in SnowProblems for each direction
        Console.WriteLine("Geben Sie für jede Himmelsrichtung die Schneeprobleme (ohne Leerzeichen, durch Komma getrennt) ein.");
        Console.WriteLine("1-Neuschnee, 2-Triebschnee, 3-Nassschnee, 4-Altschnee, 5-Gleitschnee");
        for (int i = 0; i < 8; i++)     //for each celestial direction
        {
            List<SnowProblem> SPList = new List<SnowProblem>();             //new List for the SnowProblems of each direction
            Console.WriteLine($"{Enum.GetName(typeof(Direction), i)}:");    //Print the direction 
            input=Console.ReadLine();                                       //read the SnowProblems from the Console
            while (input==null)
            {
                Console.WriteLine("Eingabe nicht erkannt. Bitte wiederholen.");
                input=Console.ReadLine();
            }
            string[] inputs=input.Split(',');
            while (true)
            {
                try
                {
                    foreach (string item in inputs)
                    {
                        SPList.Add((SnowProblem)((Convert.ToInt32(item)-1)));   
                    }
                    break;
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Eingabe nicht erkannt. Bitte wiederholen.");
                    input=Console.ReadLine();
                     while (input==null)
                    {
                        Console.WriteLine("Eingabe nicht erkannt. Bitte wiederholen.");
                        input=Console.ReadLine();
                    }
                    inputs=input.Split(',');
                }
            }
            
            SnowProblem_Direction.Add((Direction)i,SPList);     //Add the the celestial direction with the dependet list of SnowProblems to the dictionary 
        }

        Console.Clear();
        printReport();
    }

    public AvalancheStatusReport(object test) //Constructor Importing an examplary AvalancheStatusreport (the one given in Wiki under "Entwurf")
    {
        const int subdivision_upperLimit=1800;                          //exemplary values
        const AvalancheLevel ALunderSubdiv = AvalancheLevel.mäßig;
        const int totalUpperLimit=9000;
        const AvalancheLevel ALoverSubdiv=AvalancheLevel.erheblich;
        const SnowProblem MainSnowProblem = SnowProblem.Triebschnee;
        const SnowProblem OtherSnowProblem = SnowProblem.Gleitschnee;

        SnowProblem_Direction=new Dictionary<Direction, List<SnowProblem>>();
        AvalancheLevel_ac2Height=new List<AvalancheLevel_Height>();

        AvalancheLevel_ac2Height.Add(new AvalancheLevel_Height(subdivision_upperLimit,ALunderSubdiv));
        AvalancheLevel_ac2Height.Add(new AvalancheLevel_Height(totalUpperLimit,ALoverSubdiv)); 

        for (int i = 0; i < 8; i++)     //Snowproblems for every Exposition
        {
            List<SnowProblem> SPList = new List<SnowProblem>();
            SPList.Add(MainSnowProblem);
            if (2<=i&&i<=6)             //from East to West
            {
                SPList.Add(OtherSnowProblem);
            }
            SnowProblem_Direction.Add((Direction)i,SPList);
        }

        Console.Clear();
        printReport();
    }
}