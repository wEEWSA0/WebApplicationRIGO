namespace WebApplicationRIGO.Controllers.Dtos;

public class TripEditParameters
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateOnly? DepartureTime { get; set; }

    public string? DeparturePlace { get; set; }

    public string? ArrivalPlace { get; set; }
    
    public bool? TripType { get; set; }

    public int? MaxPassengers { get; set; }

    public string? ImageUrl { get; set; }
}