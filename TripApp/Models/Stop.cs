namespace TripApp.Models;

public class Stop
{
    public int Id { get; set; }
    public double StopLat { get; set; }
    public double StopLon { get; set; }

    public int TripId { get; set; }
    public Trip Trip { get; set; } = null!;
}