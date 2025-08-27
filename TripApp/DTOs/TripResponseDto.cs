namespace TripApp.DTOs;

public class TripResponseDto
{
    public int TripId { get; set; }
    public string Distance { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Polyline { get; set; } = string.Empty;
}