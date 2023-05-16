using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebApplicationRIGO.Controllers.Dtos;
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
    public IActionResult AddNew(Trip trip)
    {
        try
        {
            _tripsRepository.AddNew(trip);
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR!!! " + e.Message);

            return StatusCode(502, "База данных умерла");
        }

        return StatusCode(201);
    }
    
    [HttpPost("FetchTrips")]
    public List<Trip> GetTripsByParameters(TripParameters tripParameters)
    {
        var parameters = tripParameters;
        
        return _tripsRepository.GetTripsByParameters(parameters.DeparturePlace, parameters.ArrivalPlace, parameters.DepartureTime, parameters.TripType);
    }

    [HttpPost("SetActive")]
    public IActionResult SetTripActive(TripIdWithActive tripIdWithActive)
    {
        int result = 500;
        
        try
        {
            result = _tripsRepository.SetTripActive(tripIdWithActive.TripId, tripIdWithActive.IsActive);
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR!!! " + e.Message);

            return StatusCode(502, "База данных умерла");
        }
        
        return StatusCode(result);
    }
    
    [HttpPost("Edit")]
    public IActionResult EditTrip(TripEditParameters tripParameters)
    {
        int result = 500;
        
        try
        {
            result = _tripsRepository.EditTrip(tripParameters);
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR!!! " + e.Message);

            return StatusCode(502, "База данных умерла");
        }
        
        return StatusCode(result);
    }
    
    // [HttpPost("TestImage")]
    // public IActionResult TestImageSerivce()
    // {
    //     int result = 3;
    //
    //     // var request = new RestRequest("address/update", Method.Post).AddJsonBody(body, ContentType.Json);
    //     // var response = await client.PostAsync<AddressUpdateResponse>(request);
    //     
    //     return StatusCode(result);
    // }
}