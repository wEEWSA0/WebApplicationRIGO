using Microsoft.AspNetCore.Mvc;
using WebApplicationRIGO.Controllers.Dtos;
using WebApplicationRIGO.Models;
using WebApplicationRIGO.Repository;

namespace WebApplicationRIGO.Controllers;

[ApiController]
[Route("[controller]/")]
public class PassengersController : ControllerBase
{
    private PassengersRepository _passengersRepository = new PassengersRepository();
    private TripsRepository _tripsRepository = new TripsRepository();

    [HttpGet("GetAll")]
    public List<Passenger> GetAll()
    {
        return _passengersRepository.GetAll();
    }
    
    [HttpGet("GetTripsIds/{userId}")]
    public List<int> GetUserTripsIds(int userId)
    {
        return _passengersRepository.GetPassengerTripsIds(userId);
    }
    
    [HttpGet("GetUsersIds/{tripId}")]
    public List<int> GetTripPassengersIds(int tripId)
    {
        return _passengersRepository.GetTripPassengersIds(tripId);
    }
    
    [HttpDelete("DeleteUserFromTrip")]
    public IActionResult DeleteUserFromTrip(PassengerParameters parameters)
    {
        var result = 500;

        try
        {
            result = _passengersRepository.DeleteUserFromTrip(parameters.UserId, parameters.TripId);
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR!!! " + e.Message);

            return StatusCode(502, "База данных умерла");
        }
        
        return StatusCode(result);
    }
    
    [HttpPost("AddNew")]
    public IActionResult AddNew(Passenger passenger)
    {
        var result = 500;
        
        try
        {
            result = _passengersRepository.AddNew(passenger);
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR!!! " + e.Message);

            return StatusCode(502, "База данных умерла");
        }
        
        return StatusCode(result);
    }
    
    [HttpGet("GetAllActiveUserTrips/{userId}")]
    public IActionResult GetAllActiveUserTrips(int userId)
    {
        int result = 500;
        List<Trip> userTrips = new List<Trip>();

        try
        {
            userTrips = _tripsRepository.GetTripsByUserId(userId); // созданные юзером поездки
            
            var ids = GetUserTripsIds(userId); // id всех поездок
            for (int i = 0; i < ids.Count; i++)
            {
                userTrips.AddRange(_tripsRepository.GetById(ids[i])); // инфа о поездках по id
            }

            result = 202;
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR!!! " + e.Message);

            return StatusCode(502, "База данных умерла");
        }
        
        return StatusCode(result, userTrips);
    }
    
    [HttpGet("GetUserAsPassengerTrips/{userId}")]
    public IActionResult GetAllActiveUserAsPassengerTrips(int userId)
    {
        int result = 500;
        List<Trip> userTrips = new List<Trip>();

        try
        {
            var ids = GetUserTripsIds(userId); // id всех поездок
            for (int i = 0; i < ids.Count; i++)
            {
                userTrips.AddRange(_tripsRepository.GetById(ids[i])); // инфа о поездках по id
            }

            result = 202;
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR!!! " + e.Message);

            return StatusCode(502, "База данных умерла");
        }
        
        return StatusCode(result, userTrips);
    }
}