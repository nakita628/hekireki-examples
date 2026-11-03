#nullable enable

using System;

namespace Papermark.Models;

public partial class OAuthRecord
{
    public string Id { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public string? GrantId { get; set; }

    public string? UserCode { get; set; }

    public string? Uid { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? ConsumedAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
