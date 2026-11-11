#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMSpendLogToolIndex
{
    public string RequestId { get; set; } = null!;

    public string ToolName { get; set; } = null!;

    public DateTime StartTime { get; set; }
}
