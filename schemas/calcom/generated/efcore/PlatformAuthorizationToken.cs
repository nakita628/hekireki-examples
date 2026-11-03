#nullable enable

using System;

namespace Calcom.Models;

public partial class PlatformAuthorizationToken
{
    public string Id { get; set; } = null!;

    public virtual User Owner { get; set; } = null!;

    public virtual PlatformOAuthClient Client { get; set; } = null!;

    public string PlatformOAuthClientId { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }
}
