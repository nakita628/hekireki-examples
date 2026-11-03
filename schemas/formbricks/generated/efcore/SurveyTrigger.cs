#nullable enable

using System;

namespace Formbricks.Models;

public partial class SurveyTrigger
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Survey Survey { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public virtual ActionClass ActionClass { get; set; } = null!;

    public string ActionClassId { get; set; } = null!;
}
