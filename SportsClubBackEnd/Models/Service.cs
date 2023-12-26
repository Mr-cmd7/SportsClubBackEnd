using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SportsClubBackEnd.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public int? Cost { get; set; }

    [JsonIgnore]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
