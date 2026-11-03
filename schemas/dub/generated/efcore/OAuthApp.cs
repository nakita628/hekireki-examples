#nullable enable

using System.Collections.Generic;

namespace Dub.Models;

public partial class OAuthApp
{
    public string Id { get; set; } = null!;

    public string IntegrationId { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string HashedClientSecret { get; set; } = null!;

    public string PartialClientSecret { get; set; } = null!;

    public string RedirectUris { get; set; } = null!;

    public bool Pkce { get; set; } = false;

    public virtual ICollection<OAuthCode> OAuthCodes { get; set; } = new List<OAuthCode>();

    public virtual Integration? Integration { get; set; }
}
