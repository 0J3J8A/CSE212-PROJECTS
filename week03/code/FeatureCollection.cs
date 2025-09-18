public class FeatureCollection
{
    // TODO Problem 5 - ADD YOUR CODE HERE
    // Create additional classes as necessary
    public string Type { get; set; }
    public Metadata Metadata { get; set; }
    public List<Feature> Features { get; set; }
}

public class Metadata // assignee each property of the file (Api, Count etc..)
{
    public long Generated { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string Api { get; set; }
    public int Count { get; set; }
}

public class Feature
{
    public string Type { get; set; }
    public Properties Properties { get; set; } // asignee the Properties class
    public Geometry Geometry { get; set; } // asignee the Geometry class
    public string Id { get; set; }
}

public class Properties //create the Properties class
{
    public decimal Mag { get; set; }
    public string Place { get; set; }
    public long Time { get; set; }
    public long Updated { get; set; }
    public string Url { get; set; }
    public string Detail { get; set; }
    public int? Felt { get; set; }
    public decimal? Cdi { get; set; }
    public decimal? Mmi { get; set; }
    public string Alert { get; set; }
    public string Status { get; set; }
    public int Tsunami { get; set; }
    public int Sig { get; set; }
    public string Net { get; set; }
    public string Code { get; set; }
    public string Ids { get; set; }
    public string Sources { get; set; }
    public string Types { get; set; }
    public int? Nst { get; set; }
    public decimal? Dmin { get; set; }
    public decimal? Rms { get; set; }
    public decimal? Gap { get; set; }
    public string MagType { get; set; }
    public string Type { get; set; }
}

public class Geometry //create the Geometry class
{
    public string Type { get; set; }
    public List<decimal> Coordinates { get; set; }
}