#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class InAppAgentRun
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public virtual InAppAgentConversation Conversation { get; set; } = null!;

    public string? TriggeredByUserId { get; set; }

    public virtual User? TriggeredByUser { get; set; }

    public string? Model { get; set; }

    public string? McpApiKeyId { get; set; }

    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime? FinishedAt { get; set; }

    public string? Status { get; set; }

    public string? Request { get; set; }

    public DateTime? ClaimedAt { get; set; }

    public DateTime? HeartbeatAt { get; set; }

    public DateTime? CancelRequestedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<InAppAgentEvent> Events { get; set; } = new List<InAppAgentEvent>();
}
