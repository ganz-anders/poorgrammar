struct WorkingPosition  //struct used from the SimulationSystem class in order to create Route (queue of Working Positions)
{
    public int time;  // the time in ms, that user (+system) rest at this position
    public Position Position;

    public WorkingPosition(int time, int x, int y)  //constructor
    {
        this.time=time;
        Position=new Position(x, y);
    }
}