#nullable enable

using System;

namespace Blinko.Models;

public partial class Fonts
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string? Url { get; set; }

    public byte[]? FileData { get; set; }

    public bool IsLocal { get; set; } = false;

    public bool IsSystem { get; set; } = false;

    public string Weights { get; set; } = null!;

    public string Category { get; set; } = "sans-serif";

    public int SortOrder { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
