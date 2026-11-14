#nullable enable

using System;

namespace LangWatch.Models;

public partial class DashboardFavourite
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? DashboardId { get; set; }

    public string? TemplateId { get; set; }

    public string ProjectId { get; set; } = null!;

    public int Position { get; set; }

    public DateTime CreatedAt { get; set; }
}
