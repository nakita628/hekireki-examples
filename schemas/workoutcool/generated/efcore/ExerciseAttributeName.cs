#nullable enable

using System;
using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class ExerciseAttributeName
{
    public string Id { get; set; } = null!;

    public ExerciseAttributeNameEnum Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ExerciseAttributeValue> Values { get; set; } = new List<ExerciseAttributeValue>();

    public virtual ICollection<ExerciseAttribute> Attributes { get; set; } = new List<ExerciseAttribute>();
}
