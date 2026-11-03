#nullable enable

using System;

namespace Langfuse.Models;

public partial class InAppAgentPendingToolApproval
{
    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public virtual InAppAgentConversation Conversation { get; set; } = null!;

    public string ToolCallId { get; set; } = null!;

    public string ApprovalFingerprint { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
