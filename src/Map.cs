using System.Globalization;
class Map
{
    private const float Grid=250.0f; //Map grid in m
    private Position NWReference; //Reference point at the North-West End of the map
    private Position SEReference; //Reference point at the South-East End of the map
    private string UTMZoneReference;
    private double[][] MapData; // !!! like a matrix the first value is the y value (latitude) and the second is the x (logitude)
    public bool PositionOnMap(Position position)    //checks if position is between the reference positions (=>on map)
    {
        return NWReference.longitude<position.longitude&&position.longitude<SEReference.longitude
        &&NWReference.latitude<position.latitude&&position.latitude<SEReference.latitude;
    }
    public int getGradient(Position position)   //calculates the Gradient at the position
    {
        if (!PositionOnMap(position))           //Exception if position out of map
        {
            throw new Exception("Out of the Map.");
        }

        // linear aproximation in two directions
        double xGrad, yGrad, Grad;              
        double x=(position.longitude-NWReference.longitude)/Grid;               //the value of the position relativ to the NWReference point ->MapData entry nr.
        double y=(position.latitude-NWReference.latitude)/Grid;
        double P11=MapData[(int)Math.Truncate(y)][(int)Math.Truncate(x)];       //four auxilary points (the next point on the grid in each direction)
        double P12=MapData[(int)Math.Truncate(y)][(int)Math.Truncate(x)+1];
        double P22=MapData[(int)Math.Truncate(y)+1][(int)Math.Truncate(x)+1];
        double P21=MapData[(int)Math.Truncate(y)+1][(int)Math.Truncate(x)];

        yGrad=(((P21-P11)/Grid)+((P22-P12)/Grid))/2;
        xGrad=(((P12-P11)/Grid)+((P22-P21)/Grid))/2;

        yGrad=Math.Abs(yGrad);                          //only the absolut values ,bc. gradient can be in every direction
        xGrad=Math.Abs(xGrad);

        Grad=Math.Sqrt(xGrad*xGrad+yGrad*yGrad);        //accumulatin the gradient in x and y direction
        
        Grad=Math.Atan(Grad)*(180/Math.PI);             //returning the gradient in degree

        Grad=Math.Round(Grad);


        return((int)Grad);
    }
    public Direction? getDirection(Position position)       //calculates the exposition of the handed over position based on the gradient
    {
        if (!PositionOnMap(position))
        {
            throw new Exception("Out of the Map.");
        }
        double xGrad, yGrad;
        double x=(position.longitude-NWReference.longitude)/Grid;                   //the value of the position relativ to the NWReference point ->MapData entry nr.
        double y=(position.latitude-NWReference.latitude)/Grid;
        double P11=MapData[(int)Math.Truncate(y)][(int)Math.Truncate(x)];           //four auxilary points (the next point on the grid in each direction)
        double P12=MapData[(int)Math.Truncate(y)][(int)Math.Truncate(x)+1];
        double P22=MapData[(int)Math.Truncate(y)+1][(int)Math.Truncate(x)+1];
        double P21=MapData[(int)Math.Truncate(y)+1][(int)Math.Truncate(x)];
        yGrad=(((P21-P11)/Grid)+((P22-P12)/Grid))/2;
        xGrad=(((P12-P11)/Grid)+((P22-P21)/Grid))/2;

        switch (xGrad)                              //analyzing the gradient in x and y direction
        {
            case >0.5f: //West-Exposition
                switch (yGrad)
                {
                    case >0.5f: //North-Exp
                        return Direction.NW;
                    case <-0.5f:
                        return Direction.SW;
                    default:
                        return Direction.W;
                }
            case <-0.5f: //East
                switch (yGrad)
                {
                    case >0.5f: //North-Exp
                        return Direction.NO;
                    case <-0.5f: //South-Exp
                        return Direction.SO;
                    default:
                        return Direction.O;
                }
            default:    
                switch (yGrad)
                {
                    case >0.5f: //North-Exp
                        return Direction.N;
                    case <-0.5f: //South-Exp
                        return Direction.S;
                    default:
                        return null;
                }
        }

    }
    public int getHeightAboveSL(Position position) //coded acording to http://supercomputingblog.com/graphics/coding-bilinear-interpolation/
    {
        if (!PositionOnMap(position))
        {
            throw new Exception("Out of the Map.");
        }
        double R1,R2,P;
        double x=(position.longitude-NWReference.longitude)/Grid;               //the value of the position relativ to the NWReference point ->MapData entry nr.
        double y=(position.latitude-NWReference.latitude)/Grid;
        double Q11=MapData[(int)Math.Truncate(y)+1][(int)Math.Truncate(x)];     //four auxilary points (the next point on the grid in each direction)
        double Q12=MapData[(int)Math.Truncate(y)][(int)Math.Truncate(x)];
        double Q22=MapData[(int)Math.Truncate(y)][(int)Math.Truncate(x)+1];
        double Q21=MapData[(int)Math.Truncate(y)+1][(int)Math.Truncate(x)+1];
        
        //R1=Q11+((Q21-Q11)/Grid)*(x-Math.Truncate(x));
        //R2=Q12+((Q22-Q12)/Grid)*(x-Math.Truncate(x));

        R1=(((Math.Truncate(x)+Grid-x)/Grid)*Q11+((x-Math.Truncate(x))/Grid)*Q21);          //linear interpolation between Q11,Q21 and Q12, Q22
        R2=(((Math.Truncate(x)+Grid-x)/Grid)*Q12+((x-Math.Truncate(x))/Grid)*Q22);

        //P=R1+((R2-R1)/Grid)*(y-Math.Truncate(y));

        P=-(((Math.Truncate(y)-y)/(Grid))*R1 + ((y-(Math.Truncate(y)+Grid))/(Grid))*R2);    //second linear interpolation between R1 and R2

        P=Math.Round(P);

        return (int)P;

    }
    public Map()        //map constructor
    {
        const string fileaddress="data/map.txt";        //const fileaddres for the map file
        string? buffer;
        string[] inputs;
        int xsize=0, ysize=0;
        StreamReader sr;

        NumberFormatInfo provider = new NumberFormatInfo();
        provider.NumberDecimalSeparator = ".";                  //cultureInfo important for correct converting further down

        Console.WriteLine("Karte einlesen...");
        try
        {
            sr = new StreamReader(fileaddress);   
        }
        catch (System.Exception)
        {
            Console.WriteLine("Error. File not found. deleted or moved.");
            throw;
        }
        
        try
        {
            buffer=sr.ReadLine();           //reading the Reference point
            if (buffer!=null)
            {
                inputs=buffer.Split(' ');                           //read in reference point
                UTMZoneReference=inputs[1];
                NWReference.longitude=Convert.ToInt32(inputs[2]);
                NWReference.latitude=Convert.ToInt32(inputs[3]);
            }else
            {
                throw new Exception();
            }
            buffer=sr.ReadLine();               //reading the map size
            if (buffer!=null)
            {
                inputs=buffer.Split(',');                           //read in mapsize (for Array size)
                xsize=Convert.ToInt32(inputs[0]);
                ysize=Convert.ToInt32(inputs[1]);
                if (!(xsize>0&&ysize>0))
                {
                    Console.WriteLine("Map size not detected right");
                    throw new Exception();
                }
            }else
            {
                throw new Exception();
            }

            MapData= new double[xsize][];       //map is new array of arrays
            for (int i = 0; i < xsize; i++)
            {
                MapData[i]=new double[ysize];
            }
            for (int i = 0; i < ysize; i++)     //reading the map
            {
                buffer=sr.ReadLine();
                if (buffer!=null)
                {
                    inputs=buffer.Split("\t");
                    for (int j = 0; j < xsize; j++)
                    {
                        MapData[j][i]=Convert.ToDouble(inputs[j],provider);
                    }
                }else
                {
                    throw new Exception();
                }
            }

            SEReference.longitude=NWReference.longitude+(int)Grid*xsize;        //fill the SE reference point for easy calculation of PositionOnMap
            SEReference.latitude=NWReference.latitude+(int)Grid*ysize;
        }
        catch (System.Exception)
        {
            Console.WriteLine("Error while reading map.");
            throw;
        }
    }
}