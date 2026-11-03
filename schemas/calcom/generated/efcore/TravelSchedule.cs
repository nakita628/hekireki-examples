#nullable enable

using System;

namespace Calcom.Models;

public partial class TravelSchedule
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public string TimeZone { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? PrevTimeZone { get; set; }
}
