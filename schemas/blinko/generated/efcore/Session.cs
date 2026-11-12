#nullable enable

using System;

namespace Blinko.Models;

public partial class Session
{
    public string Id { get; set; } = null!;

    public string Sid { get; set; } = null!;

    public string Data { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
