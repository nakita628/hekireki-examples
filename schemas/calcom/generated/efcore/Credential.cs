#nullable enable

using System.Collections.Generic;

namespace Calcom.Models;

public partial class Credential
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string? EncryptedKey { get; set; }

    public virtual User? User { get; set; }

    public int? UserId { get; set; }

    public virtual Team? Team { get; set; }

    public int? TeamId { get; set; }

    public virtual App? App { get; set; }

    public string? AppId { get; set; }

    public string? SubscriptionId { get; set; }

    public string? PaymentStatus { get; set; }

    public int? BillingCycleStart { get; set; }

    public virtual ICollection<DestinationCalendar> DestinationCalendars { get; set; } = new List<DestinationCalendar>();

    public virtual ICollection<SelectedCalendar> SelectedCalendars { get; set; } = new List<SelectedCalendar>();

    public bool? Invalid { get; set; } = false;

    public virtual ICollection<CalendarCache> CalendarCache { get; set; } = new List<CalendarCache>();

    public virtual ICollection<BookingReference> References { get; set; } = new List<BookingReference>();

    public string? DelegationCredentialId { get; set; }

    public virtual DelegationCredential? DelegationCredential { get; set; }

    public virtual ICollection<IntegrationAttributeSync> IntegrationAttributeSyncs { get; set; } = new List<IntegrationAttributeSync>();

    public virtual ICollection<HostLocation> HostLocations { get; set; } = new List<HostLocation>();
}
