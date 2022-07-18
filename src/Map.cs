class Map
{
    private const float Grid=10.0f; //Map grid in m
    private Position NWReference; //Reference point at the North-West End of the map
    private Position SEReference; //Reference point at the South-East End of the map
    private string UTMZoneReference;
    private float[][] MapData; //like in a matrix the first value is the y value and the second is the x
    public bool PositionOnMap(Position position)
    {
        return NWReference.longitude<position.longitude&&position.longitude<SEReference.longitude
        &&NWReference.latitude<position.latitude&&position.latitude<SEReference.latitude;
    }
    public int getGradient(Position position)
    {
        if (!PositionOnMap(position))
        {
            throw new Exception("Out of the Map.");
        }
        double xGrad, yGrad, Grad;
        double x=position.longitude;
        double y=position.latitude;
        double P11=MapData[(int)Math.Truncate(x)][(int)Math.Truncate(y)];
        double P12=MapData[(int)Math.Truncate(x)+1][(int)Math.Truncate(y)];
        double P22=MapData[(int)Math.Truncate(x)+1][(int)Math.Truncate(y)+1];
        double P21=MapData[(int)Math.Truncate(x)][(int)Math.Truncate(y)+1];

        yGrad=(((P21-P11)/Grid)+((P22-P12)/Grid))/2;
        xGrad=(((P12-P11)/Grid)+((P22-P21)/Grid))/2;

        yGrad=Math.Abs(yGrad);
        xGrad=Math.Abs(xGrad);

        Grad=Math.Sqrt(xGrad*xGrad+yGrad*yGrad);
        Grad=Math.Truncate(Grad);

        return((int)Grad);
    }
    public Direction? getDirection(Position position)
    {
        if (!PositionOnMap(position))
        {
            throw new Exception("Out of the Map.");
        }
        double xGrad, yGrad;
        double x=position.longitude;
        double y=position.latitude;
        double P11=MapData[(int)Math.Truncate(x)][(int)Math.Truncate(y)];
        double P12=MapData[(int)Math.Truncate(x)+1][(int)Math.Truncate(y)];
        double P22=MapData[(int)Math.Truncate(x)+1][(int)Math.Truncate(y)+1];
        double P21=MapData[(int)Math.Truncate(x)][(int)Math.Truncate(y)+1];

        yGrad=(((P21-P11)/Grid)+((P22-P12)/Grid))/2;
        xGrad=(((P12-P11)/Grid)+((P22-P21)/Grid))/2;

        switch (yGrad)
        {
            case >0.1f: //West-Exposition
                switch (xGrad)
                {
                    case <0.1f: //North-Exp
                        return Direction.NW;
                    case >0.1f:
                        return Direction.SW;
                    default:
                        return Direction.W;
                }
            case <0.1f: //East
                switch (xGrad)
                {
                    case <0.1f: //North-Exp
                        return Direction.NO;
                    case >0.1f: //South-Exp
                        return Direction.SO;
                    default:
                        return Direction.O;
                }
            default:    
                switch (xGrad)
                {
                    case <0.1f: //North-Exp
                        return Direction.N;
                    case >0.1f: //South-Exp
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
        double x=(position.longitude-NWReference.longitude)/Grid;
        double y=(position.latitude-NWReference.latitude)/Grid;
        double Q11=MapData[(int)Math.Truncate(x)][(int)Math.Truncate(y)+1];
        double Q12=MapData[(int)Math.Truncate(x)][(int)Math.Truncate(y)];
        double Q22=MapData[(int)Math.Truncate(x)+1][(int)Math.Truncate(y)];
        double Q21=MapData[(int)Math.Truncate(x)+1][(int)Math.Truncate(y)+1];
        
        R1=((Q21-Q11)/Grid)*(x-Math.Truncate(x));
        R2=((Q22-Q12)/Grid)*(x-Math.Truncate(x));

        //R1=(((Math.Truncate(x)+Grid-x)/Grid)*Q11+((x-Math.Truncate(x))/Grid)*Q21);
        //R2=(((Math.Truncate(x)+Grid-x)/Grid)*Q12+((x-Math.Truncate(x))/Grid)*Q22);

        P=((R2-R1)/Grid)*(y-Math.Truncate(y));
        //P=((Math.Truncate(y)-y)/(Grid))*R1 + ((y-(Math.Truncate(y)+Grid))/(Grid))*R2;

        P=Math.Truncate(P);

        return (int)P;

    }
    public Map()
    {
        const string fileaddress="map.txt";
        string? buffer;
        string[] inputs;
        int xsize=0, ysize=0;
        StreamReader sr;

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
            buffer=sr.ReadLine();
            if (buffer!=null)
            {
                inputs=buffer.Split(' ');
                UTMZoneReference=inputs[1];
                NWReference.longitude=Convert.ToInt32(inputs[2]);
                NWReference.latitude=Convert.ToInt32(inputs[3]);
            }else
            {
                throw new Exception();
            }
            buffer=sr.ReadLine();
            if (buffer!=null)
            {
                inputs=buffer.Split(',');
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

            MapData= new float[xsize][];
            for (int i = 0; i < xsize; i++)
            {
                MapData[i]=new float[ysize];
            }
            for (int i = 0; i < ysize; i++)
            {
                buffer=sr.ReadLine();
                if (buffer!=null)
                {
                    inputs=buffer.Split("\t");
                    for (int j = 0; j < xsize; j++)
                    {
                        MapData[j][i]=Convert.ToSingle(inputs[j]);
                    }
                }else
                {
                    throw new Exception();
                }
            }

            SEReference.longitude=NWReference.longitude+(int)Grid*xsize;
            SEReference.latitude=NWReference.latitude+(int)Grid*ysize;
        }
        catch (System.Exception)
        {
            Console.WriteLine("Error while reading map.");
            throw;
        }
    }
}