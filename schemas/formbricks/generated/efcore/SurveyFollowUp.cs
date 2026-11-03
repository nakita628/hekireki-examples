#nullable enable

using System;

namespace Formbricks.Models;

public partial class SurveyFollowUp
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Survey Survey { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Trigger { get; set; } = null!;

    public string Action { get; set; } = null!;
}
