#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMInvitationLink
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public bool IsAccepted { get; set; } = false;

    public DateTime? AcceptedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual LiteLLMUserTable LiteLLMUserTableUser { get; set; } = null!;

    public virtual LiteLLMUserTable LiteLLMUserTableCreated { get; set; } = null!;

    public virtual LiteLLMUserTable LiteLLMUserTableUpdated { get; set; } = null!;
}
