class RiskEventArgs:EventArgs
{
    public DateTime Time;
    public RiskLevel RiskLevel;
    public List<SnowProblem> SnowProblems;
    public RiskEventArgs(DateTime time, RiskLevel riskLevel, List<SnowProblem> snowProblems)
    {
        Time=time;
        SnowProblems=snowProblems;
        RiskLevel=riskLevel;
    }
}