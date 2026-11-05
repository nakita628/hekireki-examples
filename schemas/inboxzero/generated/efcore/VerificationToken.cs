#nullable enable

using System;

namespace InboxZero.Models;

public partial class VerificationToken
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Identifier { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime Expires { get; set; }
}
