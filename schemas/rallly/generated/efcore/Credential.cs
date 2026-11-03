#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class Credential
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string ProviderAccountId { get; set; } = null!;

    public CredentialType Type { get; set; }

    public string Secret { get; set; } = null!;

    public List<string>? Scopes { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<CalendarConnection> CalendarConnections { get; set; } = new List<CalendarConnection>();

    public virtual ICollection<ConferencingConnection> ConferencingConnections { get; set; } = new List<ConferencingConnection>();

    public virtual User User { get; set; } = null!;
}
