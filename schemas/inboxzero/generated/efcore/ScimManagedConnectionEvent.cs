#nullable enable

using System;

namespace InboxZero.Models;

public partial class ScimManagedConnectionEvent
{
    public string Id { get; set; } = null!;

    public string ConnectionRecordId { get; set; } = null!;

    public virtual ScimManagedConnection Scimmanagedconnection { get; set; } = null!;

    public string EventKey { get; set; } = null!;

    public int Sequence { get; set; }

    public string Type { get; set; } = null!;

    public string ActorId { get; set; } = null!;

    public string? CredentialId { get; set; }

    public DateTime CreatedAt { get; set; }
}
