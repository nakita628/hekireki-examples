#nullable enable

using System;
using System.Collections.Generic;

namespace Typebot.Models;

public partial class Typebot
{
    public string Id { get; set; } = null!;

    public string? Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Icon { get; set; }

    public string Name { get; set; } = null!;

    public string? FolderId { get; set; }

    public string Groups { get; set; } = null!;

    public string? Events { get; set; }

    public string Variables { get; set; } = null!;

    public string Edges { get; set; } = null!;

    public string Theme { get; set; } = null!;

    public string? SelectedThemeTemplateId { get; set; }

    public string Settings { get; set; } = null!;

    public string? PublicId { get; set; }

    public string? CustomDomain { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public string? ResultsTablePreferences { get; set; }

    public virtual DashboardFolder? Folder { get; set; }

    public virtual Workspace Workspace { get; set; } = null!;

    public virtual ICollection<CollaboratorsOnTypebots> Collaborators { get; set; } = new List<CollaboratorsOnTypebots>();

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual PublicTypebot? PublishedTypebot { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<Webhook> Webhooks { get; set; } = new List<Webhook>();

    public bool IsArchived { get; set; } = false;

    public bool IsClosed { get; set; } = false;

    public string? WhatsAppCredentialsId { get; set; }

    public int? RiskLevel { get; set; }

    public virtual ICollection<BannedIp> BannedIps { get; set; } = new List<BannedIp>();

    public virtual Space? Space { get; set; }

    public string? SpaceId { get; set; }
}
