#nullable enable

using System;

namespace Calcom.Models;

public partial class HolidayCache
{
    public string Id { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public string CalendarId { get; set; } = null!;

    public string EventId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public int Year { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
