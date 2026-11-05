#nullable enable

using System;

namespace InboxZero.Models;

public partial class MessagingRoute
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public MessagingRoutePurpose Purpose { get; set; }

    public MessagingRouteTargetType TargetType { get; set; }

    public string TargetId { get; set; } = null!;

    public string MessagingChannelId { get; set; } = null!;

    public virtual MessagingChannel MessagingChannel { get; set; } = null!;
}
