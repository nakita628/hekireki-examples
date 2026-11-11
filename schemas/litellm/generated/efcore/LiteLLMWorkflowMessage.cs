#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMWorkflowMessage
{
    public string MessageId { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int SequenceNumber { get; set; }

    public string? SessionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual LiteLLMWorkflowRun Run { get; set; } = null!;
}
