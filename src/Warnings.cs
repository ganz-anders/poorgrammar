class Warnings
{
    public void PushMessage(RiskEventArgs args)
    {
        Console.WriteLine($"Achtung. Lawinengefahr. {args.Time}");
        Console.WriteLine($"Risiko für einen Lawinenabgang: {Enum.GetName(args.RiskLevel)}");
        Console.WriteLine($"Achten Sie besonders auf folgende Schneeprobleme: ");
        foreach (SnowProblem item in args.SnowProblems)
        {
            Console.WriteLine($"   - {Enum.GetName(item)}");
        }

    }
    public void Sound(RiskEventArgs args)
    {
        Console.Beep();
        Thread.Sleep(10);
        Console.Beep();
        Thread.Sleep(10);
        Console.Beep();
    }
    public void MessagewithSound(RiskEventArgs args)
    {
        Sound(args);
        PushMessage(args);
    }
    public void MessagewithSoundandFlashingLight(RiskEventArgs args)
    {
        Sound(args);
        for (int i = 0; i < 5; i++)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();
            Thread.Sleep(100);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Thread.Sleep(50);
        }
        PushMessage(args);
    }
}