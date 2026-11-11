#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMSearchToolsTable
{
    public string SearchToolId { get; set; } = null!;

    public string SearchToolName { get; set; } = null!;

    public string LitellmParams { get; set; } = null!;

    public string? SearchToolInfo { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
