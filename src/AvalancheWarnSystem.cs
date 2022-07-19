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
            OnPositionChanged(new PositionChangedEventArgs(position, DateTime.Now));
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
            case RiskLevel.mittel:
               if (OnRiskmid!=null)
               {
                 OnRiskmid(new RiskEventArgs(DateTime.Now, myRisk, myAVSReport.getSnowProblem_Direction(myExposition)));
               }
                break;
            case RiskLevel.hoch:
                if (OnRiskhigh!=null)
                {
                    OnRiskhigh(new RiskEventArgs(DateTime.Now, myRisk, myAVSReport.getSnowProblem_Direction(myExposition));
                }
                break;
            default:
                break;
        }
    }
    private void InitiateLogging()
    {
        const string Logfilepath="data/LogDatei.txt";
        StreamWriter sw = new StreamWriter(Logfilepath);
        myLogging = new Logging(sw);
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

        if (selfConfig)
        {
            try
            {
                //
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
        }
    }
    private RiskLevel[][] RiskMatrixFromTxt(string path)
    {

    }
    public AvalancheWarnSystem()
    {
        const string RiskMatrixPath="data/RiskMatrix.txt";
        myAVSReport=new AvalancheStatusReport();
        thisMap=new Map();
        ConfigurateWarnings();
        RiskMatrix=RiskMatrixFromTxt(RiskMatrixPath);
    }
}