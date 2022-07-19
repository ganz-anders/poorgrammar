class Logging
{
    private static string? Filepath;
    public static void LogPosition(object? caller, PositionChangedEventArgs args)
    {
        if(Filepath!=null)
        {
            StreamWriter sw = new StreamWriter(Filepath, append: true);
            sw.WriteLine($"__________\nPosition {args.Time} : {args.Position}\n__________");
            sw.Close();
        } else 
        {
            Console.WriteLine("Error! Logging not possible.");
        }
    }
    public static void LogWarning(object? caller,RiskEventArgs args)
    {
        if(Filepath!=null)
        {
            StreamWriter sw = new StreamWriter(Filepath, append : true);
            sw.WriteLine($"----------\nLawinengefahr.{args.Time}");
            sw.WriteLine($"Risiko für einen Lawinenabgang: {Enum.GetName(args.RiskLevel)}");
            sw.WriteLine($"Besonders folgende Schneeprobleme: ");
            foreach (SnowProblem item in args.SnowProblems)
            {
                sw.WriteLine($"   - {Enum.GetName(item)}");
            }
            sw.WriteLine("----------");
            sw.Close();
        } else 
        {
            Console.WriteLine("Error! Logging not possible.");
        }
    }

    public Logging(string filepath)
    {
        Filepath=filepath; 
    }
}