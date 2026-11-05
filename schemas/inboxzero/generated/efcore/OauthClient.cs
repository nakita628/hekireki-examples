#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class OauthClient
{
    public string Id { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string? ClientSecret { get; set; }

    public string? ClientDiscoveryId { get; set; }

    public bool? Disabled { get; set; } = false;

    public bool? SkipConsent { get; set; }

    public bool? EnableEndSession { get; set; }

    public string? SubjectType { get; set; }

    public List<string>? Scopes { get; set; }

    public List<string>? ClientCredentialsScopes { get; set; } = new List<string>();

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Name { get; set; }

    public string? Uri { get; set; }

    public string? Icon { get; set; }

    public List<string>? Contacts { get; set; }

    public string? Tos { get; set; }

    public string? Policy { get; set; }

    public string? SoftwareId { get; set; }

    public string? SoftwareVersion { get; set; }

    public string? SoftwareStatement { get; set; }

    public List<string>? RedirectUris { get; set; }

    public List<string>? PostLogoutRedirectUris { get; set; }

    public string? BackchannelLogoutUri { get; set; }

    public bool? BackchannelLogoutSessionRequired { get; set; }

    public string? TokenEndpointAuthMethod { get; set; }

    public string? ApplicationType { get; set; }

    public string? Jwks { get; set; }

    public string? JwksUri { get; set; }

    public List<string>? GrantTypes { get; set; }

    public List<string>? ResponseTypes { get; set; }

    public bool? RequirePKCE { get; set; }

    public bool? DpopBoundAccessTokens { get; set; } = false;

    public string? ReferenceId { get; set; }

    public string? Metadata { get; set; }

    public virtual ICollection<OauthClientResource> OauthClientResources { get; set; } = new List<OauthClientResource>();

    public virtual ICollection<OauthRefreshToken> Oauthrefreshtokens { get; set; } = new List<OauthRefreshToken>();

    public virtual ICollection<OauthAccessToken> Oauthaccesstokens { get; set; } = new List<OauthAccessToken>();

    public virtual ICollection<OauthConsent> Oauthconsents { get; set; } = new List<OauthConsent>();
}
