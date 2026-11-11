#nullable enable

using System;

namespace Linkwarden.Models;

public partial class EmailVerificationCode
{
    public string Identifier { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int Attempts { get; set; } = 0;

    public DateTime Expires { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
