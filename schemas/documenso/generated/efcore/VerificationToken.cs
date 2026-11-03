#nullable enable

using System;

namespace Documenso.Models;

public partial class VerificationToken
{
    public int Id { get; set; }

    public string SecondaryId { get; set; } = null!;

    public string Identifier { get; set; } = null!;

    public string Token { get; set; } = null!;

    public bool Completed { get; set; } = false;

    public DateTime Expires { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Metadata { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
