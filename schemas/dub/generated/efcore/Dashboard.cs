#nullable enable

using System;

namespace Dub.Models;

public partial class Dashboard
{
    public string Id { get; set; } = null!;

    public virtual Link? Link { get; set; }

    public string? LinkId { get; set; }

    public virtual Folder? Folder { get; set; }

    public string? FolderId { get; set; }

    public virtual Project? Project { get; set; }

    public string? ProjectId { get; set; }

    public virtual User? User { get; set; }

    public string? UserId { get; set; }

    public bool DoIndex { get; set; } = false;

    public string? Password { get; set; }

    public bool ShowConversions { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
