#nullable enable

using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class ProgramSuggestedSet
{
    public string Id { get; set; } = null!;

    public string ProgramSessionExerciseId { get; set; } = null!;

    public int SetIndex { get; set; }

    public List<WorkoutSetType>? Types { get; set; } = new List<WorkoutSetType>();

    public List<int>? ValuesInt { get; set; } = new List<int>();

    public List<int>? ValuesSec { get; set; } = new List<int>();

    public List<WorkoutSetUnit>? Units { get; set; } = new List<WorkoutSetUnit>();

    public virtual ProgramSessionExercise ProgramSessionExercise { get; set; } = null!;
}
