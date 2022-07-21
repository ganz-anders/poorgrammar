struct Position
{
    public int longitude; //x (east-west)
    public int latitude; //y (north-south)
    public override string ToString()   //To String Method for easy Console/StreamWriter Output
    {
        return "N"+longitude.ToString()+" O"+latitude.ToString();
    }
    public Position(int x, int y)   //Constructor for easy use of the struct
    {
        longitude=x;
        latitude=y;
    }
}