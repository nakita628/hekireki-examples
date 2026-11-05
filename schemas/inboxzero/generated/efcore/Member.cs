#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Member
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public string Role { get; set; } = "member";

    public bool AllowOrgAdminAnalytics { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<ConversationParticipant> ConversationParticipants { get; set; } = new List<ConversationParticipant>();

    public virtual ICollection<ConversationComment> ConversationComments { get; set; } = new List<ConversationComment>();
}
