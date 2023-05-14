using Microsoft.AspNetCore.Mvc;
using WebApplicationRIGO.DbContext;
using WebApplicationRIGO.Models;

namespace WebApplicationRIGO.Repository;

public class UsersRepository
{
    private WeewsaRigoDbContext _dbContext;

    public UsersRepository()
    {
        _dbContext = new WeewsaRigoDbContext();
    }

    public List<User> GetAll()
    {
        return _dbContext.Users.ToList();
    }

    public void AddNew(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public bool IsUserExistsByEmail(string email)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

        return user != null;
    }
    
    public bool IsUserExistsByEmail(string email, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

        return user != null;
    }
    
    public User? GetUserIfUserExistsByEmailAndPassword(string email, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

        return user;
    }
    
    public User Get(int id)
    {
        return _dbContext.Users.Where(u => u.Id == id).ToList()[0];
    }
    
    public int SetUser(User newUser)
    {
        User? user = _dbContext.Users.FirstOrDefault(u => u.Id == newUser.Id);

        if (user == null)
        {
            return 418;
        }
        
        if (user.Email != newUser.Email)
        {
            if (IsUserExistsByEmail(newUser.Email))
            {
                return 418;
            }
                
            user.Email = newUser.Email;
        }

        user.Name = newUser.Name;
        user.Password = newUser.Password;
        user.ContactUrl = newUser.ContactUrl;
        user.PhoneNumber = newUser.PhoneNumber;
        
        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
        
        return 202;
    }
}

/*
    public bool IsUserExistsByEmailAndPassword(string email, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

        return user != null;
    }
*/