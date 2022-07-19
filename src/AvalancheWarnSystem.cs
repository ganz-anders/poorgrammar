class AvalancheWarnSystem
{
    public event EventHandler<PositionChangedEventArgs>? OnPositionChanged;
    public event EventHandler<RiskEventArgs>? OnRiskmid;
    public event EventHandler<RiskEventArgs>? OnRiskhigh;
    private Logging? myLogging;
    public Map thisMap;
    public AvalancheStatusReport myAVSReport;
    public RiskLevel[][] RiskMatrix;        //Matrix[Gradient][AVLevel] wich contains an RiskLevel
                                            //Gradient 0 represents <30°, 1 repr. <35°, 2 repr. <40°, 3 repr. >=40°
    private Position CurrentPosition;
    public void manipulatePosition(Position position)
    {
        this.CurrentPosition=position;
        if (OnPositionChanged!=null)
        {
            OnPositionChanged(this, new PositionChangedEventArgs(position, DateTime.Now));
        }
    }
    public void EvaluatePosition()
    {
        Position myPosition = CurrentPosition;
        int myGradient=thisMap.getGradient(myPosition);
        Direction? myExposition=thisMap.getDirection(myPosition);
        int myHeight=thisMap.getHeightAboveSL(myPosition);
        RiskLevel myRisk;

        switch (myGradient)
        {
            case <30:
                myRisk=RiskMatrix[0][(int)myAVSReport.getAvalancheLevel_Height(myHeight)];
                break;
            case <35:
                myRisk=RiskMatrix[1][(int)myAVSReport.getAvalancheLevel_Height(myHeight)];
                break;
            case <40:
                myRisk=RiskMatrix[2][(int)myAVSReport.getAvalancheLevel_Height(myHeight)];
                break;
            case <90:
                myRisk=RiskMatrix[3][(int)myAVSReport.getAvalancheLevel_Height(myHeight)];
                break;
            default:
                myRisk=RiskLevel.hoch;
                Console.WriteLine("Error. Fehler bei der Berechnung des Gradienten.");
                break;
        }

        switch (myRisk)
        {
            case RiskLevel.niedrig:
                //everything is fine
                break;
            case RiskLevel.mittel:
               if (OnRiskmid!=null)
               {
                    if (myExposition!=null)
                        {
                            OnRiskmid(this, new RiskEventArgs(myPosition, DateTime.Now, myRisk, myAVSReport.getSnowProblem_Direction((Direction)myExposition)));   
                        }else
                        {
                            OnRiskmid(this, new RiskEventArgs(myPosition, DateTime.Now, myRisk, new List<SnowProblem>()));
                        }
               }
                break;
            case RiskLevel.hoch:
                if (OnRiskhigh!=null)
                {
                    if (myExposition!=null)
                    {
                        OnRiskhigh(this, new RiskEventArgs(myPosition, DateTime.Now, myRisk, myAVSReport.getSnowProblem_Direction((Direction)myExposition)));   
                    }else
                    {
                        OnRiskhigh(this, new RiskEventArgs(myPosition, DateTime.Now, myRisk, new List<SnowProblem>()));
                    }
                }
                break;
            default:
                break;
        }
    }

    public void CountinuousEvaluatePosition(object? sleepTime)
    {
        const int StandardSleepTime=500;
        int SleepTime;
        if (sleepTime!=null)
        {
            try
            {
                SleepTime = Convert.ToInt32(sleepTime as int?);
            }
            catch (System.Exception)
            {
                Console.WriteLine("SleepTime of the continuous Position Evaluation not processible. Loading Standards...");
                SleepTime=StandardSleepTime;
            }
        } else 
        {
            SleepTime=StandardSleepTime;
        }
        while (true)
        {
            Console.Write(".");
            EvaluatePosition();
            Thread.Sleep(SleepTime);
        }
    }
    private void InitiateLogging()
    {
        const string Logfilepath="data/LogDatei.txt";
        myLogging = new Logging(Logfilepath);
        
        myAVSReport.printReport(Logfilepath);

        OnPositionChanged+= new EventHandler<PositionChangedEventArgs>(Logging.LogPosition);
        OnRiskmid+= new EventHandler<RiskEventArgs>(Logging.LogWarning);
        OnRiskhigh+= new EventHandler<RiskEventArgs>(Logging.LogWarning);
        Console.WriteLine("Logging eingerichtet. Sie finden die Datei unter " + Logfilepath);
    }
    private void ConfigurateWarnings()
    {
        Console.WriteLine("Wollen Sie die Warnungen selbst einstellen? [y/n]\nAnsonsten werden die Standards geladen.");
        string? input=Console.ReadLine();
        bool isinput=false;
        bool selfConfig=false;
        do
        {
            switch (input)
            {
                case "y":
                    isinput=true;
                    selfConfig=true;
                    break;
                case "n":
                    isinput=true;
                    selfConfig=false;
                    break;
                case "Y":
                    isinput=true;
                    selfConfig=true;
                    break;
                case "N":
                    isinput=true;
                    selfConfig=false;
                    break;
                default:
                    Console.WriteLine("Eingabe nicht erkannt.\nWollen Sie die Warnungen selbst einstellen? [y/n]");
                    input=Console.ReadLine();
                    break;
            }   
        } while (!isinput);

        if (selfConfig) //self configuration of warnings
        {
            try
            {
                //Self config of Logging (on/off)
                Console.WriteLine("Wollen Sie eine LogDatei schreiben? [y/n]");
                input=Console.ReadLine();
                isinput=false;
                do
                {
                    switch (input)
                    {
                        case "y":
                            isinput=true;
                            InitiateLogging();
                            break;
                        case "n":
                            isinput=true;
                            Console.WriteLine("Logging: aus");
                            break;
                        case "Y":
                            isinput=true;
                            InitiateLogging();
                            break;
                        case "N":
                            isinput=true;
                            Console.WriteLine("Logging: aus");
                            break;
                        default:
                            Console.WriteLine("Eingabe nicht erkannt.\nWollen Sie eine LogDatei schreiben? [y/n]");
                            input=Console.ReadLine();
                            break;
                    }   
                } while (!isinput);

                //Self config of warnings
                Console.WriteLine("Welche Warnungen wollen Sie bei erhöhtem Risiko (mittlere Stufe)?");
                Console.WriteLine("1 - Push Benachrichtigung\n2 - Ton\n3 - Benachrichtigung mit Ton\n4 - Benachrichtigung mit Ton und Blinklicht\n0 - Abbrechen Standards laden");
                input=Console.ReadLine();
                isinput=false;
                do
                {
                    switch (input)
                    {
                        case "1":
                            isinput=true;
                            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.PushMessage);
                            break;
                        case "2":
                            isinput=true;
                            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.Sound);
                            break;
                        case "3":
                            isinput=true;
                            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.PushMessage);
                            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.Sound);
                            break;
                        case "4":
                            isinput=true;
                            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.MessagewithFlashingLight);
                            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.Sound);
                            break;
                        case "0":
                            throw new Exception();
                        default:
                            Console.WriteLine("Eingabe nicht erkannt.\nWelche Warnungen wollen Sie bei erhöhtem Risiko (mittlere Stufe)?");
                            input=Console.ReadLine();
                            break;
                    }   
                } while (!isinput);
                
                //selfconfig of high risk warnings
                Console.WriteLine("Welche Warnungen wollen Sie bei hohem Risiko (oberste Stufe)?");
                Console.WriteLine("1 - Push Benachrichtigung\n2 - Ton\n3 - Benachrichtigung mit Ton\n4 - Benachrichtigung mit Ton und Blinklicht\n0 - Abbrechen-Standards laden");
                input=Console.ReadLine();
                isinput=false;
                do
                {
                    switch (input)
                    {
                        case "1":
                            isinput=true;
                            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.PushMessage);
                            break;
                        case "2":
                            isinput=true;
                            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.Sound);
                            break;
                        case "3":
                            isinput=true;
                            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.PushMessage);
                            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.Sound);
                            break;
                        case "4":
                            isinput=true;
                            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.MessagewithFlashingLight);
                            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.Sound);
                            break;
                        case "0":
                            throw new Exception();
                        default:
                            Console.WriteLine("Eingabe nicht erkannt.\nWelche Warnungen wollen Sie bei erhöhtem Risiko (mittlere Stufe)?");
                            input=Console.ReadLine();
                            break;
                    }   
                } while (!isinput);

            }
            catch (System.Exception)
            {
                selfConfig=false;       //if self config failed standards should be loaded without another questioning
                Console.Write("Error. ");
            }
        }

        if (!selfConfig)    //also true if selfconfig failed
        {
            Console.WriteLine("Standards werden geladen.");
            InitiateLogging();
            
            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.PushMessage);
            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.Sound);

            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.MessagewithFlashingLight);
            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.Sound);
        }
    }

    private RiskLevel[][] RiskMatrixFromTxt(string path)
    {
        RiskLevel[][] ReturnMatrix= new RiskLevel[4][];
            
        for (int i = 0; i < ReturnMatrix.Length; i++)
        {
            ReturnMatrix[i]= new RiskLevel[5];
        }

        try
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader(path);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Error. Risk Matrix not found. Moved or deleted?");
                throw;
            }
            string? buffer;
            string[] inputs;
            
            for (int i = 0; i < 4; i++)
            {
                buffer=sr.ReadLine();
                if (buffer==null)
                {
                    throw new Exception("Line 1 to 4 can't be empty");
                }
                inputs=buffer.Split(",");
                for (int j = 0; j < 5; j++)
                {
                    ReturnMatrix[i][j]=(RiskLevel)(Convert.ToInt32(inputs[j])-1);
                }
            }

        }
        catch (System.Exception)
        {
            Console.WriteLine("Error hwile Reading Risk matrix");
            throw;
        }

        return ReturnMatrix;
    }

    public AvalancheWarnSystem()
    {
        const string RiskMatrixPath="data/RiskMatrix.txt";
        RiskMatrix=RiskMatrixFromTxt(RiskMatrixPath);
        myAVSReport=new AvalancheStatusReport();
        thisMap=new Map();
        ConfigurateWarnings();
    }
    public AvalancheWarnSystem(object test)
    {
        const string RiskMatrixPath="data/RiskMatrix.txt";
        RiskMatrix=RiskMatrixFromTxt(RiskMatrixPath);
        myAVSReport=new AvalancheStatusReport(test);
        thisMap=new Map();
        ConfigurateWarnings();
    }
}