#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class PasswordResetTokens
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Users User { get; set; } = null!;
}
