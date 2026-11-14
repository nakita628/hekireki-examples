#nullable enable

using System;

namespace LangWatch.Models;

public partial class EmailSuppression
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? TriggerId { get; set; }

    public string Reason { get; set; } = "unsubscribe";

    public DateTime CreatedAt { get; set; }
}
