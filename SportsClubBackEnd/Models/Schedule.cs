using System;
using System.Collections.Generic;

namespace SportsClubBackEnd.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? ServiceId { get; set; }

    public int? VisitorId { get; set; }

    public int? TrainerId { get; set; }

    public int? DayOfWeek { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool? Conducted { get; set; }

    public virtual Service? Service { get; set; }

    public virtual Trainer? Trainer { get; set; }

    public virtual Visitor? Visitor { get; set; }
}
