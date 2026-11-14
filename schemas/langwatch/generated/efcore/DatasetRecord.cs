#nullable enable

using System;

namespace LangWatch.Models;

public partial class DatasetRecord
{
    public string Id { get; set; } = null!;

    public string DatasetId { get; set; } = null!;

    public virtual Dataset Dataset { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Entry { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
