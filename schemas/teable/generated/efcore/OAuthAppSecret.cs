#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class OAuthAppSecret
{
    public string Id { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public string MaskedSecret { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUsedTime { get; set; }

    public virtual ICollection<OAuthAppToken> Tokens { get; set; } = new List<OAuthAppToken>();

    public virtual OAuthApp OAuthApp { get; set; } = null!;
}
