#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class TemporaryAuthTokens
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Users User { get; set; } = null!;
}
