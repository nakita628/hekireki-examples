#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class DelegationCredential
{
    public string Id { get; set; } = null!;

    public virtual WorkspacePlatform WorkspacePlatform { get; set; } = null!;

    public int WorkspacePlatformId { get; set; }

    public string ServiceAccountKey { get; set; } = null!;

    public bool Enabled { get; set; } = false;

    public DateTime? LastEnabledAt { get; set; }

    public DateTime? LastDisabledAt { get; set; }

    public int OrganizationId { get; set; }

    public virtual Team Organization { get; set; } = null!;

    public string Domain { get; set; } = null!;

    public virtual ICollection<SelectedCalendar> SelectedCalendars { get; set; } = new List<SelectedCalendar>();

    public virtual ICollection<DestinationCalendar> DestinationCalendar { get; set; } = new List<DestinationCalendar>();

    public virtual ICollection<BookingReference> BookingReferences { get; set; } = new List<BookingReference>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Credential> Credentials { get; set; } = new List<Credential>();
}
