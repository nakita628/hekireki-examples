#nullable enable

using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class WorkoutSet
{
    public string Id { get; set; } = null!;

    public string WorkoutSessionExerciseId { get; set; } = null!;

    public int SetIndex { get; set; }

    public WorkoutSetType Type { get; set; }

    public List<WorkoutSetType>? Types { get; set; } = new List<WorkoutSetType>();

    public List<int>? ValuesInt { get; set; } = new List<int>();

    public List<int>? ValuesSec { get; set; } = new List<int>();

    public List<WorkoutSetUnit>? Units { get; set; } = new List<WorkoutSetUnit>();

    public bool Completed { get; set; } = false;

    public virtual WorkoutSessionExercise WorkoutSessionExercise { get; set; } = null!;
}
