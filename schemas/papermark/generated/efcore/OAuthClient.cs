#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class OAuthClient
{
    public string Id { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string? ClientUri { get; set; }

    public string? LogoUri { get; set; }

    public List<string>? RedirectUris { get; set; }

    public List<string>? GrantTypes { get; set; }

    public string Scopes { get; set; } = null!;

    public string TokenAuthMethod { get; set; } = null!;

    public string? HashedSecret { get; set; }

    public string RegistrationType { get; set; } = null!;

    public string? MetadataUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<RestrictedToken> Tokens { get; set; } = new List<RestrictedToken>();
}
