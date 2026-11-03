#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class OauthResource
{
    public string Id { get; set; } = null!;

    public string Identifier { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? AccessTokenTtl { get; set; }

    public int? RefreshTokenTtl { get; set; }

    public string? SigningAlgorithm { get; set; }

    public string? SigningKeyId { get; set; }

    public List<string>? AllowedScopes { get; set; } = new List<string>();

    public string? CustomClaims { get; set; }

    public bool? DpopBoundAccessTokensRequired { get; set; } = false;

    public bool? Disabled { get; set; } = false;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? PolicyVersion { get; set; } = 1;

    public string? Metadata { get; set; }

    public virtual ICollection<OauthClientResource> Clients { get; set; } = new List<OauthClientResource>();
}
