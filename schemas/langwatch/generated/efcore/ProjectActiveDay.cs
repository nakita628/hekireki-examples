#nullable enable

using System;

namespace LangWatch.Models;

public partial class ProjectActiveDay
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Day { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
