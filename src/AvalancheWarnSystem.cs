class AvalancheWarnSystem
{
    public event EventHandler<PositionChangedEventArgs> OnPositionChanged;
    public event EventHandler<RiskEventArgs> OnRiskmid;
    public event EventHandler<RiskEventArgs> OnRiskhigh;
    public Map thisMap;
    public AvalancheStatusReport myAVSReport;
    public RiskLevel[][] RiskMatrix;        //Matrix[Gradient][AVLevel] wich contains an RiskLevel
                                            //Gradient 0 represents <30°, 1 repr. <35°, 2 repr. <40°, 3 repr. >=40°
    public Position CurrentPosition {get; private set}
    public void manipulatePosition(Position position)
    {
        this.CurrentPosition=position;
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
                OnRiskmid(new RiskEventArgs(DateTime.Now, myRisk, myAVSReport.getSnowProblem_Direction(myExposition)));
                break;
            case RiskLevel.hoch:
                OnRiskhigh(new RiskEventArgs(DateTime.Now, myRisk, myAVSReport.getSnowProblem_Direction(myExposition));
                break;
            default:
                break;
        }
    }
    private void ConfigurateWarnings()
    {

    }
    private void ConfigurateMap()
    {

    }
    private int[][] RiskMatrixFromTxt(fp FilePointer)
    {

    }
    public AvalancheWarnSystem()
}