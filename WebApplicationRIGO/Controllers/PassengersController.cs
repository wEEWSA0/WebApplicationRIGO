using Microsoft.AspNetCore.Mvc;
using WebApplicationRIGO.Repository;

namespace WebApplicationRIGO.Controllers;

[ApiController]
[Route("[controller]/")]
public class PassengersController : ControllerBase
{
    private PassengersRepository _passengersRepository = new PassengersRepository();

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
    public IActionResult DeleteUserFromTrip(int userId, int tripId)
    {
        var result = _passengersRepository.DeleteUserFromTrip(userId, tripId);

        return StatusCode(result);
    }
    
    [HttpPost("AddNew")]
    public IActionResult AddNew(Passenger passenger)
    {
        var result = _passengersRepository.AddNew(passenger);
        
        return StatusCode(result);
    }
}