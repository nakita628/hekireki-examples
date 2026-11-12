#nullable enable

using System;

namespace Blinko.Models;

public partial class McpServers
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Type { get; set; } = null!;

    public string? Command { get; set; }

    public string? Args { get; set; }

    public string? Url { get; set; }

    public string? Env { get; set; }

    public string? Headers { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
