#nullable enable

using System;

namespace Chibisafe.Models;

public partial class Invites
{
    public int Id { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? UsedBy { get; set; }

    public string Code { get; set; } = null!;

    public bool Used { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }
}
