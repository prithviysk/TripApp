namespace TripApp.DTOs;

public class CreateTripDto
{
    public string CreatorId { get; set; }
    public double DestinationLat { get; set; }
    public double DestinationLong { get; set; }
    public List<StopDto> Stops { get; set; } = new();
}