#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class InAppAgentConversation
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string? CreatedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string? Title { get; set; }

    public DateTime? RenamedByUserAt { get; set; }

    public InAppAgentConversationVisibilityScope VisibilityScope { get; set; } = InAppAgentConversationVisibilityScope.Personal;

    public string? ProviderSessionId { get; set; }

    public List<string>? AlwaysAllowedTools { get; set; } = new List<string>();

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<InAppAgentEvent> Events { get; set; } = new List<InAppAgentEvent>();

    public virtual ICollection<InAppAgentRun> Runs { get; set; } = new List<InAppAgentRun>();

    public virtual ICollection<InAppAgentPendingToolApproval> PendingToolApprovals { get; set; } = new List<InAppAgentPendingToolApproval>();
}
