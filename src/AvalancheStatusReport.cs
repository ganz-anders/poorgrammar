struct AvalancheLevel_Height
{
    public int UpperLimit;
    public AvalancheLevel AvalancheLevel;
    public AvalancheLevel_Height(int uLimit, AvalancheLevel avalancheLevel)
    {
        UpperLimit=uLimit;
        AvalancheLevel=avalancheLevel;
    }
}
class AvalancheStatusReport
{
    private List<AvalancheLevel_Height> AvalancheLevel_ac2Height;
    private Dictionary<Direction, List<SnowProblem>> SnowProblem_Direction;
    public void printReport(StreamWriter sw)
    {
        sw.WriteLine($"Lawinen-Lage-Bericht für heute den {DateTime.Today} \n...");
        foreach (var item in AvalancheLevel_ac2Height)
        {
            sw.WriteLine($"Lawinenlagestufe bis {item.UpperLimit}m: {Enum.GetName(typeof(AvalancheLevel), item.AvalancheLevel)}");
        }

        sw.WriteLine("Hauptschneeprobleme nach Himmelsrichtung:");
        List<string> output = new List<string>();
        int n;
        for (int i = 0; i < 8; i++)
        {
            sw.Write($"{Enum.GetName(typeof(Direction), i)}: ");
            output.Clear();
            n=0;
            foreach (SnowProblem item in SnowProblem_Direction[(Direction)i])
            {
                if (Enum.GetName(item)!=null)
                {
                    output.Add(Enum.GetName(item));
                }
                n++;
            }
            sw.WriteLine(string.Join(',',output));
        }
        Console.WriteLine("_____");
    }

    public void printReport()
    {
        Console.WriteLine($"Lawinen-Lage-Bericht für heute den {DateTime.Today} \n...");
        foreach (var item in AvalancheLevel_ac2Height)
        {
            Console.WriteLine($"Lawinenlagestufe bis {item.UpperLimit}m: {Enum.GetName(typeof(AvalancheLevel), item.AvalancheLevel)}");
        }

        Console.WriteLine("Hauptschneeprobleme nach Himmelsrichtung:");
        List<string> output = new List<string>();
        int n;
        for (int i = 0; i < 8; i++)
        {
            Console.Write($"{Enum.GetName(typeof(Direction), i)}: ");
            output.Clear();
            n=0;
            foreach (SnowProblem item in SnowProblem_Direction[(Direction)i])
            {
                if (Enum.GetName(item)!=null)
                {
                    output.Add(Enum.GetName(item));
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

    public List<SnowProblem> getSnowProblem_Direction(Direction direction)
    {
        return SnowProblem_Direction[direction];
    }

    public AvalancheStatusReport()
    {
        const int totalUpperLimit=9000; //9000 is the ultimate upperLimit, bc. no mountain is higher
        bool subdiv=false;
        bool isinput=false;
        string? input;
        int upperLimit;
        AvalancheLevel? AVLevel=null;
        SnowProblem_Direction=new Dictionary<Direction, List<SnowProblem>>();
        AvalancheLevel_ac2Height=new List<AvalancheLevel_Height>();

        Console.WriteLine($"Bitte geben Sie den Lawinen-Lage-Bericht für heute den {DateTime.Now} ein");
        Console.WriteLine("...");

        //read in the Avalanche Levels dependet on height
        Console.WriteLine("Gibt es eine Unterteilung des LLB in verschiedene Höhenstufen? [y/n]?");
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

        //with subdivisions
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

            //more subdivs?
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
        
        //read in the last (most high up) AVLevel
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


        //read in SnowProblems
        Console.WriteLine("Geben Sie für jede Himmelsrichtung die Schneeprobleme (ohne Leerzeichen, durch Komma getrennt) ein.");
        Console.WriteLine("1-Neuschnee, 2-Triebschnee, 3-Nassschnee, 4-Altschnee, 5-Gleitschnee");
        for (int i = 0; i < 8; i++)
        {
            List<SnowProblem> SPList = new List<SnowProblem>();
            Console.WriteLine($"{Enum.GetName(typeof(Direction), i)}:");
            input=Console.ReadLine();
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
            
            SnowProblem_Direction.Add((Direction)i,SPList);
        }

        Console.Clear();
        printReport();
    }

    public AvalancheStatusReport(object test) //Importing an examplary AvalancheStatusreport (the one given in Wiki under "Entwurf")
    {
        const int subdivision_upperLimit=1800;
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