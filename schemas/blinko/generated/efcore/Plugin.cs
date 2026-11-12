#nullable enable

using System;

namespace Blinko.Models;

public partial class Plugin
{
    public int Id { get; set; }

    public string Metadata { get; set; } = null!;

    public string Path { get; set; } = null!;

    public bool IsUse { get; set; } = true;

    public bool IsDev { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
