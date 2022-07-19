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
