#nullable enable

using System;

namespace Langfuse.Models;

public partial class InAppAgentEvent
{
    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public virtual InAppAgentConversation Conversation { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public virtual InAppAgentRun Run { get; set; } = null!;

    public int SequenceNumber { get; set; }

    public string Type { get; set; } = null!;

    public string Event { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
