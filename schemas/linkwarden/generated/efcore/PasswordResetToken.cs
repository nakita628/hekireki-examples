#nullable enable

using System;

namespace Linkwarden.Models;

public partial class PasswordResetToken
{
    public string Identifier { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime Expires { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
