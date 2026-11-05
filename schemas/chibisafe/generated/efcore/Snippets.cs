#nullable enable

using System;

namespace Chibisafe.Models;

public partial class Snippets
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public int? UserId { get; set; }

    public string Identifier { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Content { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string? ParentUuid { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }
}
