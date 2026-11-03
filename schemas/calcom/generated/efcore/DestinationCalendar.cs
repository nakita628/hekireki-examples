#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class DestinationCalendar
{
    public int Id { get; set; }

    public string Integration { get; set; } = null!;

    public string ExternalId { get; set; } = null!;

    public string? PrimaryEmail { get; set; }

    public virtual User? User { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Booking> Booking { get; set; } = new List<Booking>();

    public virtual EventType? EventType { get; set; }

    public int? EventTypeId { get; set; }

    public int? CredentialId { get; set; }

    public virtual Credential? Credential { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual DelegationCredential? DelegationCredential { get; set; }

    public string? DelegationCredentialId { get; set; }

    public int? CustomCalendarReminder { get; set; }
}
