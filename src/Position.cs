struct Position
{
    public int longitude; //x (east-west)
    public int latitude; //y (north-south)
    public override string ToString()
    {
        return "N"+longitude.ToString()+" O"+latitude.ToString();
    }
    public Position(int x, int y)
    {
        longitude=x;
        latitude=y;
    }
}