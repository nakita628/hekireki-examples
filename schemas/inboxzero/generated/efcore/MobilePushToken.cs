#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class MobilePushToken
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Token { get; set; } = null!;

    public MobilePushPlatform Platform { get; set; }

    public MobilePushTokenType TokenType { get; set; } = MobilePushTokenType.Expo;

    public string? AppVersion { get; set; }

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<MobilePushDelivery> MobilePushDeliveries { get; set; } = new List<MobilePushDelivery>();
}
