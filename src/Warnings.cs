class Warnings
{
    public static void PushMessage(object? caller,RiskEventArgs args)   //push message warning - prints the most important information to Console
    {
        Console.WriteLine("\n_____");
        Console.WriteLine($"Achtung. Lawinengefahr. {args.Time}");
        Console.WriteLine($"Aktuelle Position: {args.Position}");
        Console.WriteLine($"Risiko für einen Lawinenabgang: {Enum.GetName(args.RiskLevel)}");
        Console.WriteLine($"Achten Sie besonders auf folgende Schneeprobleme: ");
        foreach (SnowProblem item in args.SnowProblems)
        {
            Console.WriteLine($"   - {Enum.GetName(item)}");
        }
        Console.WriteLine("_____");
    }
    public static void Sound(object? caller,RiskEventArgs args)     //sound warning (does only make sound)
    {
        Console.Beep();
    }
    // not needed anymore (Event could be linked with the both seperate delegates)
    /*public static void MessagewithSound(object? caller,RiskEventArgs args)
    {
        Sound(caller,args);
        PushMessage(caller,args);
    }*/
    public static void MessagewithFlashingLight(object? caller,RiskEventArgs args)      //Push-message, but with flashing lights before
    {
        //Sound(caller, args);
        for (int i = 0; i < 5; i++)         //changes the color of the console window
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();
            Thread.Sleep(100);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Thread.Sleep(50);
        }
        PushMessage(caller, args);      //calls the Push Message method
    }
}