using System;
using System.Collections.Generic;

namespace WebApplicationRIGO.Models;

public partial class Trip
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CreatorId { get; set; }

    public bool IsActive { get; set; }

    public DateOnly DepartureTime { get; set; }

    public string DeparturePlace { get; set; } = null!;

    public string ArrivalPlace { get; set; } = null!;

    public bool TripType { get; set; }

    public int MaxPassengers { get; set; }

    public string? ImageUrl { get; set; }
}
