#nullable enable

using System;

namespace LangWatch.Models;

public partial class WebPushSubscription
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Endpoint { get; set; } = null!;

    public string P256dh { get; set; } = null!;

    public string Auth { get; set; } = null!;

    public string? UserAgent { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastSuccessAt { get; set; }
}
