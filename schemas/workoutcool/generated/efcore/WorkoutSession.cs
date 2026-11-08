#nullable enable

using System;
using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class WorkoutSession
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public int? Duration { get; set; }

    public virtual ICollection<WorkoutSessionExercise> Exercises { get; set; } = new List<WorkoutSessionExercise>();

    public List<ExerciseAttributeValueEnum>? Muscles { get; set; } = new List<ExerciseAttributeValueEnum>();

    public int? Rating { get; set; }

    public string? RatingComment { get; set; }

    public virtual UserSessionProgress? UserSessionProgress { get; set; }
}
