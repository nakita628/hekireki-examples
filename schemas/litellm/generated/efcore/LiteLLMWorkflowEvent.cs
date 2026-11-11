#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMWorkflowEvent
{
    public string EventId { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public string EventType { get; set; } = null!;

    public string StepName { get; set; } = null!;

    public int SequenceNumber { get; set; }

    public string? Data { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual LiteLLMWorkflowRun Run { get; set; } = null!;
}
