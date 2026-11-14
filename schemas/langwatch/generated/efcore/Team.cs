#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Team
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public virtual ICollection<TeamUser> Members { get; set; } = new List<TeamUser>();

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public bool IsPersonal { get; set; } = false;

    public string? OwnerUserId { get; set; }

    public virtual User? OwnerUser { get; set; }

    public virtual ICollection<IngestionSource> IngestionSources { get; set; } = new List<IngestionSource>();

    public virtual ICollection<AiToolEntryTeam> AiToolEntryTeams { get; set; } = new List<AiToolEntryTeam>();

    public string? DepartmentId { get; set; }
}
