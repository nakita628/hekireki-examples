#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class OAuthApp
{
    public string Id { get; set; } = null!;

    public string? OrganizationId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? PictureId { get; set; }

    public string RedirectUrl { get; set; } = null!;

    public string? RedirectUris { get; set; }

    public string ClientId { get; set; } = null!;

    public string? ClientSecret { get; set; }

    public bool Dynamic { get; set; } = false;

    public string? TokenEndpointAuthMethod { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual Media? Picture { get; set; }

    public virtual ICollection<OAuthAuthorization> Authorizations { get; set; } = new List<OAuthAuthorization>();

    public virtual ICollection<OAuthSelfHostedAuthorization> SelfHostedAuthorizations { get; set; } = new List<OAuthSelfHostedAuthorization>();
}
