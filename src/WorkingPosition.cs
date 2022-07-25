struct WorkingPosition
{
    public int time;  // the time in ms, that the system rests at this position
    public Position Position;

    public WorkingPosition(int time, int x, int y)
    {
        this.time=time;
        Position=new Position(x, y);
    }
}