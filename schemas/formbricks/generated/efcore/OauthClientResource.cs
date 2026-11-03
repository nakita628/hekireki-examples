#nullable enable

using System;

namespace Formbricks.Models;

public partial class OauthClientResource
{
    public string Id { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public virtual OauthClient Client { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public virtual OauthResource Resource { get; set; } = null!;

    public string? Metadata { get; set; }

    public DateTime? CreatedAt { get; set; }
}
