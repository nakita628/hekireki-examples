#nullable enable

using System;
using System.Collections.Generic;

namespace Jitsu.Models;

public partial class OAuthClient
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ClientSecretHash { get; set; } = null!;

    public string Name { get; set; } = null!;

    public List<string>? RedirectUris { get; set; }

    public virtual ICollection<UserApiToken> Tokens { get; set; } = new List<UserApiToken>();
}
