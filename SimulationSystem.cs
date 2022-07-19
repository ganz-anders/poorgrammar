struct WorkingPosition
{
    public int time;  // the time in ms, that the system rests at this position
    public Position Position;

    public WorkingPosition(int time, int x, int y)
    {
        this.time=time;
        Position=new Position(x, y);
    }
}

class SimulationSystem
{ 
    private static AvalancheWarnSystem? myWarnSystem;
    private static List<WorkingPosition>? Route;
    private static List<WorkingPosition> ReadinTestRoute(string filepath)
    {
        string? buffer;
        string[] inputs;
        StreamReader sr = new StreamReader(filepath);
        List<WorkingPosition> ReturnRoute = new List<WorkingPosition>();

        while (true)
        {
            buffer=sr.ReadLine();
            if (buffer!=null)
            {
                inputs=buffer.Split();
                ReturnRoute.Add(new WorkingPosition(Convert.ToInt32(inputs[0]), Convert.ToInt32(inputs[1]), Convert.ToInt32(inputs[2])));
            } else
            {
                break;
            }
        }

        return ReturnRoute;
    }
    private static void PositionSimulation()
    {
        if (Route!=null)
        {
            if (myWarnSystem!=null)
            {
                foreach (WorkingPosition pos in Route)
                {
                    myWarnSystem.manipulatePosition(pos.Position);
                    Thread.Sleep(pos.time);
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
        const string Routefilepath="data/TestRoute.txt";
        Route = ReadinTestRoute(Routefilepath);
        myWarnSystem = new AvalancheWarnSystem();

        //start execution of Threads
        Thread PositionSimul = new Thread(SimulationSystem.PositionSimulation);
        Thread PositionEvaluation= new Thread(myWarnSystem.EvaluatePosition);
    }
}