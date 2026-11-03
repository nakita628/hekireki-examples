#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class AccessCode
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? ClientId { get; set; }

    public virtual OAuthClient? Client { get; set; }

    public DateTime ExpiresAt { get; set; }

    public List<AccessScope>? Scopes { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public int? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public string? CodeChallenge { get; set; }

    public string? CodeChallengeMethod { get; set; } = "S256";
}
