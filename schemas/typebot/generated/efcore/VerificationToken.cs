#nullable enable

using System;

namespace Typebot.Models;

public partial class VerificationToken
{
    public string Identifier { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string? Value { get; set; }

    public int FailedAttempts { get; set; } = 0;

    public DateTime Expires { get; set; }
}
