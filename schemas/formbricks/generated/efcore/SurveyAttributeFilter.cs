#nullable enable

using System;

namespace Formbricks.Models;

public partial class SurveyAttributeFilter
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ContactAttributeKey AttributeKey { get; set; } = null!;

    public string AttributeKeyId { get; set; } = null!;

    public virtual Survey Survey { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public SurveyAttributeFilterCondition Condition { get; set; }

    public string Value { get; set; } = null!;
}
