#nullable enable

using System;

namespace Dub.Models;

public partial class PasswordResetToken
{
    public string Identifier { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime Expires { get; set; }
}
