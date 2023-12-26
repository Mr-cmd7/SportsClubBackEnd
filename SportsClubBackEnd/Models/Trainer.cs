using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SportsClubBackEnd.Models;

public partial class Trainer
{
    public int TrainerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
