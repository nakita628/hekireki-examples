#nullable enable

using System;
using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class ExerciseAttributeValue
{
    public string Id { get; set; } = null!;

    public string AttributeNameId { get; set; } = null!;

    public ExerciseAttributeValueEnum Value { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ExerciseAttributeName AttributeName { get; set; } = null!;

    public virtual ICollection<ExerciseAttribute> Attributes { get; set; } = new List<ExerciseAttribute>();
}
