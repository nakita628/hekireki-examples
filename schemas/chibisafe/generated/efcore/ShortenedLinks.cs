#nullable enable

using System;

namespace Chibisafe.Models;

public partial class ShortenedLinks
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public virtual Users? User { get; set; }

    public int? UserId { get; set; }

    public string Identifier { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public int Views { get; set; } = 0;

    public DateTime CreatedAt { get; set; }
}
