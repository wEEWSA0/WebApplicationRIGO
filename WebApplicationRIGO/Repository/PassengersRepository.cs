namespace WebApplicationRIGO.Repository;

public class PassengersRepository
{
    private WeewsaRigoDbContext _dbContext;

    public PassengersRepository()
    {
        _dbContext = new WeewsaRigoDbContext();
    }

    public List<Passenger> GetAll()
    {
        return _dbContext.Passengers.ToList();
    }
    
    public List<int> GetPassengerTripsIds(int userId)
    {
        var passengers = _dbContext.Passengers.Where(p => p.UserId == userId).ToList();

        List<int> tripsIds = new List<int>();

        foreach (var passenger in passengers)
        {
            tripsIds.Add(passenger.TripId);
        }
        
        return tripsIds;
    }
    
    public List<int> GetTripPassengersIds(int tripId)
    {
        var passengers = _dbContext.Passengers.Where(p => p.UserId == tripId).ToList();

        List<int> usersIds = new List<int>();

        foreach (var passenger in passengers)
        {
            usersIds.Add(passenger.UserId);
        }
        
        return usersIds;
    }
    
    public int DeleteUserFromTrip(int userId, int tripId)
    {
        var passengers = _dbContext.Passengers.Where(p => p.UserId == userId && p.TripId == tripId).ToList();

        if (passengers == null || passengers.Count == 0)
        {
            return 418;
        }
        
        _dbContext.Passengers.RemoveRange(passengers);
        _dbContext.SaveChanges();
        
        return 202;
    }

    public int AddNew(Passenger passenger)
    {
        var dublicatePassengers = _dbContext.Passengers.FirstOrDefault(p =>
            p.UserId == passenger.UserId && p.TripId == passenger.TripId);

        if (dublicatePassengers != null)
        {
            return 418;
        }
        
        _dbContext.Passengers.Add(passenger);
        _dbContext.SaveChanges();

        return 202;
    }
}