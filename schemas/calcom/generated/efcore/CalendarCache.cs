#nullable enable

using System;

namespace Calcom.Models;

public partial class CalendarCache
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int CredentialId { get; set; }

    public int? UserId { get; set; }

    public virtual Credential? Credential { get; set; }
}
