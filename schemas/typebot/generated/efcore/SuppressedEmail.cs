#nullable enable

using System;

namespace Typebot.Models;

public partial class SuppressedEmail
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Email { get; set; } = null!;

    public int TransientGeneralBounceCount { get; set; } = 0;

    public DateTime? SuppressedAt { get; set; }

    public string? LastWebhookId { get; set; }
}
