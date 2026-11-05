#nullable enable

using System;

namespace InboxZero.Models;

public partial class MobilePushDelivery
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string DeduplicationKey { get; set; } = null!;

    public string MobilePushTokenId { get; set; } = null!;

    public virtual MobilePushToken MobilePushToken { get; set; } = null!;
}
