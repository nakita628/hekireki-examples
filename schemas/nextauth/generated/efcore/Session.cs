#nullable enable

using System;

namespace NextAuth.Models;

public partial class Session
{
    public string SessionToken { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime Expires { get; set; }

    public virtual User User { get; set; } = null!;
}
