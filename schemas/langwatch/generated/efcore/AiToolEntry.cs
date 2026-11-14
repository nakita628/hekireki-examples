#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class AiToolEntry
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public string ScopeId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? IconKey { get; set; }

    public string? IconAsset { get; set; }

    public int Order { get; set; } = 0;

    public bool Enabled { get; set; } = true;

    public string Config { get; set; } = "{}";

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedById { get; set; }

    public string? UpdatedById { get; set; }

    public virtual ICollection<AiToolEntryTeam> Teams { get; set; } = new List<AiToolEntryTeam>();

    public virtual ICollection<AiToolEntryDepartment> Departments { get; set; } = new List<AiToolEntryDepartment>();
}
