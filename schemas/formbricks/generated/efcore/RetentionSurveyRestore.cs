#nullable enable

using System;

namespace Formbricks.Models;

public partial class RetentionSurveyRestore
{
    public virtual Survey Survey { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public DateTime RestoredAt { get; set; }
}
