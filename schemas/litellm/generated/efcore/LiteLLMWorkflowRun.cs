#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMWorkflowRun
{
    public string RunId { get; set; } = null!;

    public string SessionId { get; set; } = null!;

    public string WorkflowType { get; set; } = null!;

    public string Status { get; set; } = "pending";

    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Input { get; set; }

    public string? Output { get; set; }

    public string? Metadata { get; set; }

    public virtual ICollection<LiteLLMWorkflowEvent> Events { get; set; } = new List<LiteLLMWorkflowEvent>();

    public virtual ICollection<LiteLLMWorkflowMessage> Messages { get; set; } = new List<LiteLLMWorkflowMessage>();
}
