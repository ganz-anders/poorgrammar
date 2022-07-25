/*class for the arguments handed over to the position logging methods when the associated event is triggerd
contains the most important information about the current situation*/
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