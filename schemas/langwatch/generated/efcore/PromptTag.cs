#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class PromptTag
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<PromptTagAssignment> Assignments { get; set; } = new List<PromptTagAssignment>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedById { get; set; }

    public virtual User? CreatedBy { get; set; }

    public string? UpdatedById { get; set; }

    public virtual User? UpdatedBy { get; set; }
}
