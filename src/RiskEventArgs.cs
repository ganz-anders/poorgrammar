/*class for the arguments handed over to the warning methods when the event is triggerd
contains the most important information about the current situation*/
class RiskEventArgs:EventArgs
{
    public Position Position;
    public DateTime Time;
    public RiskLevel RiskLevel;
    public List<SnowProblem> SnowProblems;
    public RiskEventArgs(Position position, DateTime time, RiskLevel riskLevel, List<SnowProblem> snowProblems)
    {
        Position=position;
        Time=time;
        SnowProblems=snowProblems;
        RiskLevel=riskLevel;
    }
}