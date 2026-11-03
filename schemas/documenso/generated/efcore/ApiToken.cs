#nullable enable

using System;

namespace Documenso.Models;

public partial class ApiToken
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Token { get; set; } = null!;

    public ApiTokenAlgorithm Algorithm { get; set; } = ApiTokenAlgorithm.Sha512;

    public DateTime? Expires { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;
}
