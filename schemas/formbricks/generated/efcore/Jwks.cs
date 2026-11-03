#nullable enable

using System;

namespace Formbricks.Models;

public partial class Jwks
{
    public string Id { get; set; } = null!;

    public string PublicKey { get; set; } = null!;

    public string PrivateKey { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string? Alg { get; set; }

    public string? Crv { get; set; }
}
