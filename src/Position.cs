struct Position
{
    public int longitude; //x (east-west)
    public int latitude; //y (north-south)
    public override string ToString()   //To String Method for easy Console/StreamWriter Output
    {
        return "O"+longitude.ToString()+" N"+latitude.ToString();
    }
    public Position(int x, int y)   //Constructor for easy use of the struct
    {
        longitude=x;
        latitude=y;
    }
}