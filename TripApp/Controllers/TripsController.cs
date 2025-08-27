using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TripApp.Data;
using TripApp.DTOs;
using TripApp.Models;
using TripApp.Services;

namespace TripApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly GoogleApiOptions  _googleApiOptions;

    public TripsController(AppDbContext context, HttpClient httpClient, IOptions<GoogleApiOptions> googleApiOptions)
    {
        _context = context;
        _httpClient = httpClient;
        _googleApiOptions = googleApiOptions.Value;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrip([FromBody] CreateTripDto dto)
    {
        var trip = new Trip
        {
            CreatorId = dto.CreatorId,
            DestinationLat = dto.DestinationLat,
            DestinationLong = dto.DestinationLong,
            Stops = dto.Stops.Select(s => new Stop
            {
                StopLat = s.Lat,
                StopLon = s.Lng,
            }).ToList()
        };
        
        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        var origin = $"{dto.Stops.First().Lat},{dto.Stops.First().Lng}";
        var destination = $"{dto.DestinationLat},{dto.DestinationLong}";
        var wayPoints = string.Join("|", dto.Stops.Skip(1).Select(s => $"{s.Lat},{s.Lng}"));
        
        var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&key={_googleApiOptions.ApiKey}";
        
        if (!string.IsNullOrEmpty(wayPoints))
            url += $"&waypoints={wayPoints}";
        
        Console.WriteLine(url);
        
        var response = await _httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var firstRoute = root.GetProperty("routes")[0];
        var firstLeg = firstRoute.GetProperty("legs")[0];

        var distance = firstLeg.GetProperty("distance").GetProperty("text").GetString();
        var duration = firstLeg.GetProperty("duration").GetProperty("text").GetString();
        var polyline = firstRoute.GetProperty("overview_polyline").GetProperty("points").GetString();
        
        trip.Distance = distance??"";
        trip.Duration = duration??"";
        trip.Polyline = polyline??"";

        var result = new TripResponseDto
        {
            TripId = trip.Id,
            Distance = distance ?? "",
            Duration = duration ?? "",
            Polyline = polyline ?? ""
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetTrip([FromRoute] int id)
    {
        var trip = _context.Trips.Include(t=>t.Stops).FirstOrDefault(t=>t.Id == id);

        if (trip == null)
        {
            return NotFound();
        }

        var result = new TripResponseDto
        {
            TripId = trip.Id,
            Distance = trip.Distance ?? "",
            Duration = trip.Duration ?? "",
            Polyline = trip.Polyline ?? "",
        };
        
        return Ok(result);
    }
}