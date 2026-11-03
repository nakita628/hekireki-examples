#nullable enable

using System;

namespace Documenso.Models;

public partial class PasswordResetToken
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime Expiry { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
