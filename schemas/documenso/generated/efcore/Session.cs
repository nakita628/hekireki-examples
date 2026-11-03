#nullable enable

using System;

namespace Documenso.Models;

public partial class Session
{
    public string Id { get; set; } = null!;

    public string SessionToken { get; set; } = null!;

    public int UserId { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsEmbed { get; set; } = false;

    public virtual User? User { get; set; }
}
