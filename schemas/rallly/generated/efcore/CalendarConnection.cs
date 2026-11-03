#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class CalendarConnection
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string IntegrationId { get; set; } = null!;

    public string ProviderAccountId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? DisplayName { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Credential Credential { get; set; } = null!;

    public string CredentialId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<ProviderCalendar> ProviderCalendars { get; set; } = new List<ProviderCalendar>();
}
