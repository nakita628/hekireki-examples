#nullable enable

using System;

namespace Jitsu.Models;

public partial class EmailLog
{
    public string Id { get; set; } = null!;

    public DateTime Time { get; set; }

    public string Email { get; set; } = null!;

    public string? Error { get; set; }

    public string? PreviewUrl { get; set; }

    public string? MessageId { get; set; }

    public string Status { get; set; } = null!;
}
