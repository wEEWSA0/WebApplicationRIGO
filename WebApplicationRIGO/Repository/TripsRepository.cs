namespace WebApplicationRIGO.Repository;

public class TripsRepository
{
    private WeewsaRigoDbContext _dbContext;

    public TripsRepository()
    {
        _dbContext = new WeewsaRigoDbContext();
    }

    public List<Trip> GetAll()
    {
        return _dbContext.Trips.Where(t => t.IsActive).ToList();
    }

    public void AddNew(Trip trip)
    {
        _dbContext.Trips.Add(trip);
        _dbContext.SaveChanges();
    }
    
    public List<Trip> GetTripsByParameters(string? departurePlace, string? arrivalPlace, DateOnly? departureTime)
    {
        var trips = _dbContext.Trips.Where(t => t.IsActive).ToList();

        if (departurePlace != null || departurePlace == "")
        {
            trips = trips.Where(t => t.DeparturePlace == departurePlace).ToList();
        }

        if (arrivalPlace != null || arrivalPlace == "")
        {
            trips = trips.Where(t => t.ArrivalPlace == arrivalPlace).ToList();
        }

        if (departureTime != null)
        {
            trips = trips.Where(t => t.DepartureTime == departureTime.Value).ToList();
        }

        return trips.ToList();
    }
    
    public List<Trip> GetTripsByUserId(int id)
    {
        var trips = _dbContext.Trips.Where(t => t.IsActive).Where(t => t.CreatorId == id).ToList();
        
        return trips.ToList();
    }
    
    public int SetTripActive(int tripId, bool isActive)
    {
        var trip = _dbContext.Trips.FirstOrDefault(t => t.Id == tripId);
        
        if (trip == null)
        {
            return 418 + 10000*(tripId+1);
        }

        trip.IsActive = isActive;
        
        _dbContext.Trips.Update(trip);
        _dbContext.SaveChanges();
        
        return 202;
    }
}