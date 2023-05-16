namespace WebApplicationRIGO.Controllers.Dtos;

public class TripParameters
{
    public string? DeparturePlace { get; set; }
    public string? ArrivalPlace { get; set; }
    public DateOnly? DepartureTime { get; set; }
    public bool? TripType { get; set; }
}