#nullable enable

using System;

namespace Dub.Models;

public partial class ProjectUsers
{
    public string Id { get; set; } = null!;

    public WorkspaceRole Role { get; set; } = WorkspaceRole.Member;

    public string UserId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual NotificationPreference? NotificationPreference { get; set; }

    public string? WorkspacePreferences { get; set; }

    public string? DefaultFolderId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
