#nullable enable

using System;

namespace Calcom.Models;

public partial class Session
{
    public string Id { get; set; } = null!;

    public string SessionToken { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime Expires { get; set; }

    public virtual User? User { get; set; }
}
