class PositionChangedEventArgs :EventArgs
{
    public Position Position;
    public DateTime Time;

    public PositionChangedEventArgs(Position position, DateTime time)
    {
        Position=position;
        Time=time;
    }

}
