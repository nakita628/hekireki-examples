#nullable enable

using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class WorkoutSessionExercise
{
    public string Id { get; set; } = null!;

    public string WorkoutSessionId { get; set; } = null!;

    public string ExerciseId { get; set; } = null!;

    public int Order { get; set; }

    public virtual WorkoutSession WorkoutSession { get; set; } = null!;

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual ICollection<WorkoutSet> Sets { get; set; } = new List<WorkoutSet>();
}
