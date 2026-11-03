#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class OAuthApp
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Logo { get; set; }

    public string Homepage { get; set; } = null!;

    public string? Description { get; set; }

    public string ClientId { get; set; } = null!;

    public string? RedirectUris { get; set; }

    public string? Scopes { get; set; }

    public bool AllowDeviceFlow { get; set; } = false;

    public bool DynamicRegistration { get; set; } = false;

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual ICollection<OAuthAppSecret> Secrets { get; set; } = new List<OAuthAppSecret>();

    public virtual ICollection<OAuthAppAuthorized> Authorized { get; set; } = new List<OAuthAppAuthorized>();

    public virtual ICollection<OAuthAppToken> Tokens { get; set; } = new List<OAuthAppToken>();
}
