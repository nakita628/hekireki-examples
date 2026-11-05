#nullable enable

using System;

namespace InboxZero.Models;

public partial class McpTool
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Schema { get; set; }

    public bool IsEnabled { get; set; } = true;

    public bool IsWrite { get; set; } = false;

    public string ConnectionId { get; set; } = null!;

    public virtual McpConnection Connection { get; set; } = null!;
}
