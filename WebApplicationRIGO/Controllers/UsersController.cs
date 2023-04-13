using Microsoft.AspNetCore.Mvc;
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
    public User Get(int id)
    {
        return _usersRepository.Get(id);
    }
    
    [HttpPost("AddNew")]
    public IActionResult AddNew(User user)
    {
        if (_usersRepository.IsUserExistsByEmail(user.Email))
        {
            return StatusCode(208);
        }
        
        _usersRepository.AddNew(user);

        return StatusCode(200);
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
        
        int result = _usersRepository.SetUser(user);

        return StatusCode(result);
    }
}

// old code
/*
    [HttpPost("AddNew")]
    public void AddNew(User user)
    {
        _usersRepository.AddNew(user);
    }*/
/*
[HttpGet("Login/{email}/{password}")]
public bool Login(string email, string password)
{
    return _usersRepository.IsUserExistsByEmailAndPassword(email, password);
}*/