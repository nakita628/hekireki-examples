#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class ProviderCalendar
{
    public string Id { get; set; } = null!;

    public string CalendarConnectionId { get; set; } = null!;

    public string ProviderCalendarId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsPrimary { get; set; } = false;

    public string? TimeZone { get; set; }

    public bool IsSelected { get; set; } = false;

    public bool IsDeleted { get; set; } = false;

    public bool IsWritable { get; set; } = false;

    public string? ProviderData { get; set; }

    public DateTime LastSyncedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual CalendarConnection CalendarConnection { get; set; } = null!;

    public virtual ICollection<User> UserDefaultDestination { get; set; } = new List<User>();
}
