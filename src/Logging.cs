class Logging
{
    private static StreamWriter? sw;
    public static void LogPosition(object? caller, PositionChangedEventArgs args)
    {
        if(sw!=null)
        {
            sw.Write($"_____\nPosition {args.Time} : N{args.Position.longitude} W{args.Position.latitude}");
        } else 
        {
            Console.WriteLine("Error! Logging not possible.");
        }
    }
    public static void LogWarning(object? caller,RiskEventArgs args)
    {
        if(sw!=null)
        {
            sw.WriteLine($"----------Lawinengefahr.{args.Time}");
            sw.WriteLine($"Risiko für einen Lawinenabgang: {Enum.GetName(args.RiskLevel)}");
            sw.WriteLine($"Besonders folgende Schneeprobleme: ");
            foreach (SnowProblem item in args.SnowProblems)
            {
                sw.WriteLine($"   - {Enum.GetName(item)}");
            }
            sw.WriteLine("----------");
        } else 
        {
            Console.WriteLine("Error! Logging not possible.");
        }
    }

    public Logging(string filepath)
    {
        sw = new StreamWriter(filepath); 
    }
}