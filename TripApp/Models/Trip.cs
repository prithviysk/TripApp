namespace TripApp.Models;

public class Trip
{
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public double DestinationLat { get; set; }
    public double DestinationLong { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string? Distance { get; set; }
    public string? Duration { get; set; }
    public string? Polyline { get; set; }

    public ICollection<Stop> Stops { get; set; }
}