#nullable enable

using System;

namespace WorkoutCool.Models;

public partial class ExerciseAttribute
{
    public string Id { get; set; } = null!;

    public string ExerciseId { get; set; } = null!;

    public string AttributeNameId { get; set; } = null!;

    public string AttributeValueId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual ExerciseAttributeName AttributeName { get; set; } = null!;

    public virtual ExerciseAttributeValue AttributeValue { get; set; } = null!;
}
