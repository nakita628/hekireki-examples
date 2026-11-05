#nullable enable

using System;

namespace Chibisafe.Models;

public partial class Bans
{
    public int Id { get; set; }

    public string Ip { get; set; } = null!;

    public string? Reason { get; set; }

    public DateTime CreatedAt { get; set; }
}
