class Logging
{
    private string? Filepath;        //filepath to which the logging is written to
    public void LogPosition(object? caller, PositionChangedEventArgs args)   //logs the overhanded position when invocated
    {
        if(Filepath!=null)
        {
            StreamWriter sw = new StreamWriter(Filepath, append: true);                     //opens new stream to filepath, append mode
            sw.WriteLine($"__________\nPosition {args.Time} : {args.Position}\n__________");
            sw.Close();                                                                     //close stream
        } else 
        {
            Console.WriteLine("Error! Logging not possible.");
        }
    }
    public void LogWarning(object? caller,RiskEventArgs args)        //logs warning and some important information to `filepath`
    {
        if(Filepath!=null)
        {
            StreamWriter sw = new StreamWriter(Filepath, append : true);    //opens new stream, append mode
            
            //print the warning:
            sw.WriteLine($"----------\nLawinengefahr.{args.Time}");
            sw.WriteLine($"Risiko für einen Lawinenabgang: {Enum.GetName(args.RiskLevel)}");
            sw.WriteLine($"Exposition: {args.Direction}");
            sw.WriteLine($"Besonders folgende Schneeprobleme: ");
            foreach (SnowProblem item in args.SnowProblems)
            {
                sw.WriteLine($"   - {Enum.GetName(item)}");
            }
            sw.WriteLine("----------");

              //close stream
            sw.Close();
        } else 
        {
            Console.WriteLine("Error! Logging not possible.");
        }
    }

    public Logging(string filepath)             //constructor, to fill the filepath
    {
        Filepath=filepath; 
    }
}
