class SimulationSystem
{ 
    private static AvalancheWarnSystem? myWarnSystem;   //contains the WarnSystem that should be executed
    private static List<WorkingPosition>? Route;        //contains route
    private static List<WorkingPosition> ReadinTestRoute(string filepath)   //reads in Rout from file
    {
        string? buffer;
        string[] inputs;
        StreamReader sr = new StreamReader(filepath);
        List<WorkingPosition> ReturnRoute = new List<WorkingPosition>();    //new queue that will be return value

        //read every Position from file
        while (true)
        {
            buffer=sr.ReadLine();   //read line
            if (buffer!=null)       //if line was not empty
            {
                inputs=buffer.Split();  //split by blank characters
                //create new WorkingPosition and add to the Route (queue)
                ReturnRoute.Add(new WorkingPosition(Convert.ToInt32(inputs[0]), Convert.ToInt32(inputs[1]), Convert.ToInt32(inputs[2])));
            } else
            {
                break;
            }
        }

        return ReturnRoute;
    }
    private static void PositionSimulation()    //method that simulates the movement of the system
    {
        if (Route!=null)
        {
            if (myWarnSystem!=null)
            {
                foreach (WorkingPosition pos in Route)  //processes every Working Position in the Route
                {
                    myWarnSystem.manipulatePosition(pos.Position);  //change the position of the System (move forward)
                    Thread.Sleep(pos.time);                         //wait the time that System rests here (saved in WorkingPos.time) bevor next movement
                }
            } else
            {
                throw new Exception("ERROR! Kein ausführbares System gefunden.");
            }
        } else
        {
            throw new Exception("ERROR! Keine ausführbare Route gefunden.");
        }
    }
    public static void Main(string[] args)
    {
        const string Routefilepath="data/TestRoute.txt";    //const filepath where the Test route is 
        Route = ReadinTestRoute(Routefilepath);             //create new Route

        //ask user if Test Avalanche Status Report should be used
        Console.WriteLine("Wollen Sie zu Test- und Demonstrations-Zwecken den Standard-LLB verwenden? [y/n]");
        string? input=Console.ReadLine();
        bool isinput=false;
        do
        {
            switch (input)
            {
                case "y":
                    isinput=true;
                    myWarnSystem = new AvalancheWarnSystem("test"); //new WarnSystem with Test-Report
                    break;
                case "n":
                    isinput=true;
                    //further down Standard-constructor will be called
                    break;
                case "Y":
                    isinput=true;
                    myWarnSystem = new AvalancheWarnSystem("test"); //new WarnSystem with Test-Report
                    break;
                case "N":
                    isinput=true;
                    //further down Standard-constructor will be called
                    break;
                default:
                    Console.WriteLine("Eingabe nicht erkannt.\nWollen Sie zu Testzwecken den Standard-LLB verwenden? [y/n]");
                    input=Console.ReadLine();
                    break;
            }   
        } while (!isinput);

        if (myWarnSystem==null) //new WarnSystem with read in Avalanche Status Report if it is not filled yet
        {
            myWarnSystem = new AvalancheWarnSystem();
        }

        //start execution of Threads
        Thread PositionSimul = new Thread(SimulationSystem.PositionSimulation);
        Thread PositionEvaluation= new Thread(myWarnSystem.CountinuousEvaluatePosition);

        //the Position Evaluation is a Background Thread, so it stopps, if the Simulation has ended (Route is walked to the end)
        PositionEvaluation.IsBackground=true;

        //Start the Threads
        PositionSimul.Start();
        PositionEvaluation.Start(3000);
        Console.WriteLine("System is running.");

        PositionSimul.Join();
        Console.BackgroundColor=ConsoleColor.Black; //resetting the Console Background if the Simulation has finished during Flashing Lights warning
    }
}