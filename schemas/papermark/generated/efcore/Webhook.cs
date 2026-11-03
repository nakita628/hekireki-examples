#nullable enable

using System;

namespace Papermark.Models;

public partial class Webhook
{
    public string Id { get; set; } = null!;

    public string PId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public string Triggers { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
