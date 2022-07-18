class PositionChangedEvenArgs :EventArgs
{
    public Position Position;
    public DateTime Time;

    public PositionChangedEvenArgs(Position position, DateTime time)
    {
        Position=position;
        Time=time;
    }

}