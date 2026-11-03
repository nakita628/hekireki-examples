#nullable enable

using System;

namespace Taxonomy.Models;

public partial class VerificationToken
{
    public string Identifier { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime Expires { get; set; }
}
