#nullable enable

using System;
using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class Exercise
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? NameEn { get; set; }

    public string? Description { get; set; }

    public string? DescriptionEn { get; set; }

    public string? FullVideoUrl { get; set; }

    public string? FullVideoImageUrl { get; set; }

    public string? Introduction { get; set; }

    public string? IntroductionEn { get; set; }

    public string? Slug { get; set; }

    public string? SlugEn { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ExerciseAttribute> Attributes { get; set; } = new List<ExerciseAttribute>();

    public virtual ICollection<WorkoutSessionExercise> WorkoutSessionExercise { get; set; } = new List<WorkoutSessionExercise>();

    public virtual ICollection<ProgramSessionExercise> ProgramSessionExercise { get; set; } = new List<ProgramSessionExercise>();

    public virtual ICollection<UserFavoriteExercise> FavoritesByUsers { get; set; } = new List<UserFavoriteExercise>();
}
