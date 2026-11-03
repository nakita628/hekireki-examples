#nullable enable

using System;

namespace Papermark.Models;

public partial class SentEmail
{
    public string Id { get; set; } = null!;

    public EmailType Type { get; set; }

    public string Recipient { get; set; } = null!;

    public bool Marketing { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public virtual Team Team { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public string? DomainSlug { get; set; }
}
