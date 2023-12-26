using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SportsClubBackEnd.Models;

public partial class Visitor
{
    public int VisitorId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Age { get; set; }

    public int? Phone { get; set; }

    [JsonIgnore]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
