#nullable enable

using System;

namespace Chibisafe.Models;

public partial class Links
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public int UserId { get; set; }

    public int AlbumId { get; set; }

    public string Identifier { get; set; } = null!;

    public int Views { get; set; } = 0;

    public bool Enabled { get; set; } = true;

    public bool EnableDownload { get; set; } = false;

    public DateTime? ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }
}
