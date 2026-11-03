#nullable enable

using System;

namespace Papermark.Models;

public partial class YearInReview
{
    public string Id { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public string Status { get; set; } = "pending";

    public int Attempts { get; set; } = 0;

    public DateTime? LastAttempted { get; set; }

    public string? Error { get; set; }

    public string Stats { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
