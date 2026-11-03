#nullable enable

using System;

namespace Typebot.Models;

public partial class Webhook
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Url { get; set; }

    public string Method { get; set; } = null!;

    public string QueryParams { get; set; } = null!;

    public string Headers { get; set; } = null!;

    public string? Body { get; set; }

    public string TypebotId { get; set; } = null!;

    public virtual Typebot Typebot { get; set; } = null!;
}
