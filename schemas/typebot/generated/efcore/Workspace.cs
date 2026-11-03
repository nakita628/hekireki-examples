#nullable enable

using System;
using System.Collections.Generic;

namespace Typebot.Models;

public partial class Workspace
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? Icon { get; set; }

    public Plan Plan { get; set; } = Plan.Free;

    public string? StripeId { get; set; }

    public virtual ICollection<Credentials> Credentials { get; set; } = new List<Credentials>();

    public virtual ICollection<CustomDomain> CustomDomains { get; set; } = new List<CustomDomain>();

    public virtual ICollection<DashboardFolder> Folders { get; set; } = new List<DashboardFolder>();

    public virtual ICollection<MemberInWorkspace> Members { get; set; } = new List<MemberInWorkspace>();

    public virtual ICollection<Typebot> Typebots { get; set; } = new List<Typebot>();

    public virtual ICollection<WorkspaceInvitation> Invitations { get; set; } = new List<WorkspaceInvitation>();

    public int AdditionalChatsIndex { get; set; } = 0;

    public int AdditionalStorageIndex { get; set; } = 0;

    public DateTime? ChatsLimitFirstEmailSentAt { get; set; }

    public DateTime? StorageLimitFirstEmailSentAt { get; set; }

    public DateTime? ChatsLimitSecondEmailSentAt { get; set; }

    public DateTime? StorageLimitSecondEmailSentAt { get; set; }

    public virtual ClaimableCustomPlan? ClaimableCustomPlan { get; set; }

    public int? CustomChatsLimit { get; set; }

    public int? CustomStorageLimit { get; set; }

    public int? CustomSeatsLimit { get; set; }

    public bool IsQuarantined { get; set; } = false;

    public bool IsSuspended { get; set; } = false;

    public bool IsPastDue { get; set; } = false;

    public bool? IsVerified { get; set; }

    public virtual ICollection<ThemeTemplate> ThemeTemplates { get; set; } = new List<ThemeTemplate>();

    public int? ChatsHardLimit { get; set; }

    public string? Settings { get; set; }

    public DateTime? LastActivityAt { get; set; }

    public virtual ICollection<Space> Spaces { get; set; } = new List<Space>();
}
