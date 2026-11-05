#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class CalendarConnection
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Provider { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public bool IsConnected { get; set; } = true;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();
}
