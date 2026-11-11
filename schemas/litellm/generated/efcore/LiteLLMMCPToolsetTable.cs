#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMMCPToolsetTable
{
    public string ToolsetId { get; set; } = null!;

    public string ToolsetName { get; set; } = null!;

    public string? Description { get; set; }

    public string Tools { get; set; } = "[]";

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
