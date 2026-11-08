#nullable enable

using System;

namespace WorkoutCool.Models;

public partial class UserFavoriteExercise
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string ExerciseId { get; set; } = null!;

    public virtual Exercise Exercise { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
