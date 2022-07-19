class Warnings
{
    public static void PushMessage(object? caller,RiskEventArgs args)
    {
        Console.WriteLine($"Achtung. Lawinengefahr. {args.Time}");
        Console.WriteLine($"Aktuelle Position: {args.Position}");
        Console.WriteLine($"Risiko für einen Lawinenabgang: {Enum.GetName(args.RiskLevel)}");
        Console.WriteLine($"Achten Sie besonders auf folgende Schneeprobleme: ");
        foreach (SnowProblem item in args.SnowProblems)
        {
            Console.WriteLine($"   - {Enum.GetName(item)}");
        }

    }
    public static void Sound(object? caller,RiskEventArgs args)
    {
        Console.Beep();
        Thread.Sleep(10);
        Console.Beep();
        Thread.Sleep(10);
        Console.Beep();
    }
    // not needed anymore (Event could be linked with the both seperate delegates)
    /*public static void MessagewithSound(object? caller,RiskEventArgs args)
    {
        Sound(caller,args);
        PushMessage(caller,args);
    }*/
    public static void MessagewithSoundandFlashingLight(object? caller,RiskEventArgs args)
    {
        Sound(caller, args);
        for (int i = 0; i < 5; i++)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();
            Thread.Sleep(100);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Thread.Sleep(50);
        }
        PushMessage(caller, args);
    }
}