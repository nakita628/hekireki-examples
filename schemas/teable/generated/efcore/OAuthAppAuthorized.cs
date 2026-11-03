#nullable enable

using System;

namespace Teable.Models;

public partial class OAuthAppAuthorized
{
    public string Id { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime AuthorizedTime { get; set; }

    public virtual OAuthApp OAuthApp { get; set; } = null!;
}
