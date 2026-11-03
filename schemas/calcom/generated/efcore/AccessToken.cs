#nullable enable

using System;

namespace Calcom.Models;

public partial class AccessToken
{
    public int Id { get; set; }

    public string Secret { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public virtual User Owner { get; set; } = null!;

    public virtual PlatformOAuthClient Client { get; set; } = null!;

    public string PlatformOAuthClientId { get; set; } = null!;

    public int UserId { get; set; }
}
