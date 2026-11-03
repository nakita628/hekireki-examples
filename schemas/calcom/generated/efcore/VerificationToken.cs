#nullable enable

using System;

namespace Calcom.Models;

public partial class VerificationToken
{
    public int Id { get; set; }

    public string Identifier { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime Expires { get; set; }

    public int? ExpiresInDays { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public int? SecondaryEmailId { get; set; }

    public virtual SecondaryEmail? SecondaryEmail { get; set; }
}
