#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class AvailabilitySchedule
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDefault { get; set; } = false;

    public string Timezone { get; set; } = "UTC";

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<AvailabilityWindow> Windows { get; set; } = new List<AvailabilityWindow>();

    public virtual ICollection<BookingLink> BookingLinks { get; set; } = new List<BookingLink>();
}
