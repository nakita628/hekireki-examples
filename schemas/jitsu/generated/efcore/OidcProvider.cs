#nullable enable

using System;
using System.Collections.Generic;

namespace Jitsu.Models;

public partial class OidcProvider
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string ClientSecret { get; set; } = null!;

    public string? AuthorizationEndpoint { get; set; }

    public string? TokenEndpoint { get; set; }

    public string? UserinfoEndpoint { get; set; }

    public string? JwksUri { get; set; }

    public string? IntrospectionEndpoint { get; set; }

    public List<string>? Scopes { get; set; } = new List<string> { "openid", "profile", "email", "offline_access" };

    public string? Audience { get; set; }

    public string? Prompt { get; set; }

    public string UsernameClaim { get; set; } = "email";

    public string EmailClaim { get; set; } = "email";

    public string NameClaim { get; set; } = "name";

    public string GroupClaim { get; set; } = "groups";

    public bool Enabled { get; set; } = true;

    public bool AutoDiscovery { get; set; } = true;

    public virtual ICollection<OidcLoginGroup> OidcLoginGroups { get; set; } = new List<OidcLoginGroup>();
}
