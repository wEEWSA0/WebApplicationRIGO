﻿using System;
using System.Collections.Generic;

namespace WebApplicationRIGO.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string Password { get; set; } = null!;

    public string? ContactUrl { get; set; }

    public string? PhotoUrl { get; set; }
}
