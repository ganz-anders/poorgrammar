class AvalancheWarnSystem
{
    public event EventHandler<PositionChangedEventArgs>? OnPositionChanged; //event, triggerd from manipulateposition method
    public event EventHandler<RiskEventArgs>? OnRiskmid;    //event, triggerd from EvaluatePosition method, when risk is mid
    public event EventHandler<RiskEventArgs>? OnRiskhigh;   //event, triggerd from EvaluatePsoition method, when risk is high
    private Logging? myLogging;     //object of the Logging class - provides logging methods if not null
    public Map thisMap;
    public AvalancheStatusReport myAVSReport;
    public RiskLevel[][] RiskMatrix;        //Matrix[Gradient][AVLevel] which contains a RiskLevel
                                            //Gradient 0 represents <30°, 1 repr. <35°, 2 repr. <40°, 3 repr. >=40°
    private Position CurrentPosition;       //represents the current position of the system, in field usage this could be obtained from a gps sensor

    public void manipulatePosition(Position position)
    /*method for manipulating the position - only in this test implementation of the Warn System.
    In field usage, the position is obtained from a sensor (see above), so this method will be removed*/
    {
        this.CurrentPosition=position;
        if (OnPositionChanged!=null)    //log the Position if Logging is prepared (not null)
        {
            OnPositionChanged(this, new PositionChangedEventArgs(position, DateTime.Now));
        }
    }

    public void EvaluatePosition()                                  //method, which evaluates the Position regarding the risk
    {
        Position myPosition = CurrentPosition;                      //copy of the current position, so it does not change while evaluating
        int myGradient=thisMap.getGradient(myPosition);             //get the Gradient of the position from map
        Direction? myExposition=thisMap.getDirection(myPosition);   //get celestial direction from map
        int myHeight=thisMap.getHeightAboveSL(myPosition);          //get height above sealevel from map
        RiskLevel myRisk;                                           //field for the risk which will be used for assessing which (or if) Event to trigger

        //assess the gradient and get the belonging entry of the RiskMatrix with height
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
                myRisk=RiskLevel.hoch;  //risk could possibly be high - assume the worst
                Console.WriteLine("Error. Fehler bei der Berechnung des Gradienten.");
                break;
        }

        //assess the calculated risk and trigger belongig event
        switch (myRisk)
        {
            case RiskLevel.niedrig:
                //everything is fine
                break;
            case RiskLevel.mittel:
               if (OnRiskmid!=null)
               {
                    //trigger OnRiskmid event
                    if (myExposition!=null)
                        {   
                            //trigger event with new EventArgs with the Snow problems if direction is given
                            OnRiskmid(this, new RiskEventArgs(myPosition, DateTime.Now, myRisk, myAVSReport.getSnowProblem_Direction((Direction)myExposition)));
                        }else
                        {
                            //trigger event with new EventArgs without Snow Problems (empty list)
                            OnRiskmid(this, new RiskEventArgs(myPosition, DateTime.Now, myRisk, new List<SnowProblem>()));
                        }
               }
                break;
            case RiskLevel.hoch:
                if (OnRiskhigh!=null)
                {
                    //trigger OnRiskhigh event
                    if (myExposition!=null)
                    {
                        //trigger event with new EventArgs with the Snow problems if direction is given
                        OnRiskhigh(this, new RiskEventArgs(myPosition, DateTime.Now, myRisk, myAVSReport.getSnowProblem_Direction((Direction)myExposition)));   
                    }else
                    {
                        //trigger event with new EventArgs without Snow Problems (empty list)
                        OnRiskhigh(this, new RiskEventArgs(myPosition, DateTime.Now, myRisk, new List<SnowProblem>()));
                    }
                }
                break;
            default:
                break;
        }
    }

    public void CountinuousEvaluatePosition(object? sleepTime)
    //method which continously is running the EvaluatePosition method (see above) with handed over or standard offset time between the executions
    {
        const int StandardSleepTime=500;
        int SleepTime;
        if (sleepTime!=null)    //if sleepTime is handed over, use this value
        {
            try
            {
                SleepTime = Convert.ToInt32(sleepTime as int?);
            }
            catch (System.Exception)    //if handed over sleepTime not processible, use standard value
            {
                Console.WriteLine("SleepTime of the continuous Position Evaluation not processible. Loading Standards...");
                SleepTime=StandardSleepTime;
            }
        } else 
        {
            //use standard value
            SleepTime=StandardSleepTime;
        }
        while (true)
        {
            Console.Write(".");         //printing dots on Console, to show that system is running
            EvaluatePosition();         //execute EvaluatePosition method
            Thread.Sleep(SleepTime);    //then pause for sleepTime before next execution
        }
    }

    private void InitiateLogging()  //method which is called by ConfigurateWarnings if Logging should be initiated
    {
        const string Logfilepath="data/LogDatei.txt";   //constant filepath for logging file
        myLogging = new Logging(Logfilepath);           //creating new object of Logging class
        
       if (myAVSReport!=null)
       {
            myAVSReport.printReport(Logfilepath);       //print the AVS report to the log file to begin new logging session
       }

        //link the associated Logging methods to the events
        OnPositionChanged+= new EventHandler<PositionChangedEventArgs>(Logging.LogPosition);
        OnRiskmid+= new EventHandler<RiskEventArgs>(Logging.LogWarning);
        OnRiskhigh+= new EventHandler<RiskEventArgs>(Logging.LogWarning);
        Console.WriteLine("Logging eingerichtet. Sie finden die Datei unter " + Logfilepath);
    }

    private void ConfigurateWarnings()  //method which is called by constructor to configurate the warnings
    {
        //asking the user if they want to customize warnings
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

        if (selfConfig) 
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
                            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.Sound);
                            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.PushMessage);
                            break;
                        case "4":
                            isinput=true;
                            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.Sound);
                            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.MessagewithFlashingLight);
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
                            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.Sound);
                            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.PushMessage);
                            break;
                        case "4":
                            isinput=true;
                            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.Sound);
                            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.MessagewithFlashingLight);
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
                Console.Write("Abbruch. ");
            }
        }

        if (!selfConfig) //if no customizing wanted (or selfconfig failed)
        {
            Console.WriteLine("Standards werden geladen.");
            InitiateLogging();  //turn logging on
            
            //linking the warn methods to the belonging events according to standards
            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.PushMessage);
            OnRiskmid+= new EventHandler<RiskEventArgs>(Warnings.Sound);

            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.MessagewithFlashingLight);
            OnRiskhigh+= new EventHandler<RiskEventArgs>(Warnings.Sound);
        }
    }

    private RiskLevel[][] RiskMatrixFromTxt(string path)    //called from constructor - reads in RiskMatrix from file
    {
        //creating new array of arrays
        RiskLevel[][] ReturnMatrix= new RiskLevel[4][];
        for (int i = 0; i < ReturnMatrix.Length; i++)
        {
            ReturnMatrix[i]= new RiskLevel[5];
        }

        try
        {
            //new StreamReader for filepath
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
            //reading in Matrix from StreamReader
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

        return ReturnMatrix;    //return the gained matrix
    }

    public AvalancheWarnSystem()    //standard constructor of the class
    {
        const string RiskMatrixPath="data/RiskMatrix.txt";
        RiskMatrix=RiskMatrixFromTxt(RiskMatrixPath);
        myAVSReport=new AvalancheStatusReport();
        thisMap=new Map();
        ConfigurateWarnings();
    }

    public AvalancheWarnSystem(object test)     //constructor of the class loads the standard-test Avalanche Status Report for Test purposes
    {
        const string RiskMatrixPath="data/RiskMatrix.txt";
        RiskMatrix=RiskMatrixFromTxt(RiskMatrixPath);
        myAVSReport=new AvalancheStatusReport(test);
        thisMap=new Map();
        ConfigurateWarnings();
    }
}
