using Microsoft.AspNetCore.Mvc;
using WebApplicationRIGO.Models;
using WebApplicationRIGO.Repository;

namespace WebApplicationRIGO.Controllers;

[ApiController]
[Route("[controller]/")]
public class TripsController : ControllerBase
{
    private TripsRepository _tripsRepository = new TripsRepository();

    [HttpGet("GetAll")]
    public List<Trip> GetAll()
    {
        return _tripsRepository.GetAll();
    }
    
    [HttpGet("GetLast/{count}")]
    public List<Trip> GetLast(int count)
    {
        return _tripsRepository.GetLast(count);
    }
    
    [HttpGet("GetUserTrips/{id}")]
    public List<Trip> GetUserTrips(int id)
    {
        return _tripsRepository.GetTripsByUserId(id);
    }
    
    [HttpPost("AddNew")]
    public void AddNew(Trip trip)
    {
        _tripsRepository.AddNew(trip);
    }
    
    [HttpPost("FetchTrips")]
    public List<Trip> GetTripsByParameters(TripParameters tripParameters)
    {
        return _tripsRepository.GetTripsByParameters(tripParameters.DeparturePlace, tripParameters.ArrivalPlace, tripParameters.DepartureTime);
    }

    [HttpPost("SetActive")]
    public IActionResult SetTripActive(TripIdWithActive tripIdWithActive)
    {
        int result = _tripsRepository.SetTripActive(tripIdWithActive.TripId, tripIdWithActive.IsActive);
        
        return StatusCode(result);
    }
}

public class TripIdWithActive
{
    public int TripId { get; set; }
    public bool IsActive { get; set; }
}
public class TripParameters
{
    public string? DeparturePlace { get; set; }
    public string? ArrivalPlace { get; set; }
    public DateOnly? DepartureTime { get; set; }
}