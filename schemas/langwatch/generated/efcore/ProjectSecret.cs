#nullable enable

using System;

namespace LangWatch.Models;

public partial class ProjectSecret
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string EncryptedValue { get; set; } = null!;

    public string CreatedById { get; set; } = null!;

    public virtual User CreatedBy { get; set; } = null!;

    public string UpdatedById { get; set; } = null!;

    public virtual User UpdatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
