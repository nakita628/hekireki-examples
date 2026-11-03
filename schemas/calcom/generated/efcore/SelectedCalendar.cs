#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class SelectedCalendar
{
    public string Id { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public int UserId { get; set; }

    public string Integration { get; set; } = null!;

    public string ExternalId { get; set; } = null!;

    public virtual Credential? Credential { get; set; }

    public int? CredentialId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public string? GoogleChannelId { get; set; }

    public string? GoogleChannelKind { get; set; }

    public string? GoogleChannelResourceId { get; set; }

    public string? GoogleChannelResourceUri { get; set; }

    public string? GoogleChannelExpiration { get; set; }

    public string? ChannelId { get; set; }

    public string? ChannelKind { get; set; }

    public string? ChannelResourceId { get; set; }

    public string? ChannelResourceUri { get; set; }

    public DateTime? ChannelExpiration { get; set; }

    public DateTime? SyncSubscribedAt { get; set; }

    public DateTime? SyncSubscribedErrorAt { get; set; }

    public int SyncSubscribedErrorCount { get; set; } = 0;

    public string? SyncToken { get; set; }

    public DateTime? SyncedAt { get; set; }

    public DateTime? SyncErrorAt { get; set; }

    public int? SyncErrorCount { get; set; } = 0;

    public virtual DelegationCredential? DelegationCredential { get; set; }

    public string? DelegationCredentialId { get; set; }

    public string? Error { get; set; }

    public DateTime? LastErrorAt { get; set; }

    public int WatchAttempts { get; set; } = 0;

    public int UnwatchAttempts { get; set; } = 0;

    public int MaxAttempts { get; set; } = 3;

    public int? EventTypeId { get; set; }

    public virtual EventType? EventType { get; set; }

    public virtual ICollection<CalendarCacheEvent> CalendarCacheEvents { get; set; } = new List<CalendarCacheEvent>();
}
