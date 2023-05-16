using Microsoft.AspNetCore.Mvc;
using WebApplicationRIGO.Models;
using WebApplicationRIGO.Repository;

namespace WebApplicationRIGO.Controllers;

[ApiController]
[Route("[controller]/")]
public class UsersController : ControllerBase
{
    private UsersRepository _usersRepository = new UsersRepository();

    [HttpGet("GetAll")]
    public List<User> GetAll()
    {
        return _usersRepository.GetAll();
    }
    
    [HttpGet("Get/{id}")]
    public IActionResult Get(int id)
    {
        var user = _usersRepository.Get(id);

        if (user == null)
        {
            return StatusCode(418);
        }

        return StatusCode(200, user);
    }
    
    [HttpPost("AddNew")]
    public IActionResult AddNew(User user)
    {
        if (_usersRepository.IsUserExistsByEmail(user.Email))
        {
            return StatusCode(208);
        }
        
        try
        {
            _usersRepository.AddNew(user);
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR!!! " + e.Message);

            return StatusCode(502, "База данных умерла");
        }

        return StatusCode(201);
    }
    
    [HttpGet("IsRegistered/{email}")]
    public bool IsRegistered(string email)
    {
        return _usersRepository.IsUserExistsByEmail(email);
    }
    
    [HttpGet("Login/{email}/{password}")]
    public int Login(string email, string password)
    {
        User? user = _usersRepository.GetUserIfUserExistsByEmailAndPassword(email, password);

        if (user == null)
        {
            return -1;
        }
        else
        {
            return user.Id;
        }
    }
    
    [HttpPost("SetUser")]
    public IActionResult SetUser(User user)
    {
        if (user.Id == null || user.Password == "")
        {
            return StatusCode(418);
        }
        
        int result = 500;
        
        try
        {
            result = _usersRepository.SetUser(user);
        }
        catch (Exception e)
        {
            Console.WriteLine("ERROR!!! " + e.Message);

            return StatusCode(502, "База данных умерла");
        }

        return StatusCode(result);
    }
}