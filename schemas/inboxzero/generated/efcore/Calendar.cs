#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Calendar
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string CalendarId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Primary { get; set; } = false;

    public bool IsEnabled { get; set; } = true;

    public string? Timezone { get; set; }

    public string ConnectionId { get; set; } = null!;

    public virtual CalendarConnection Connection { get; set; } = null!;

    public virtual ICollection<BookingLink> BookingLinks { get; set; } = new List<BookingLink>();
}
