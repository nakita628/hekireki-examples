#nullable enable

using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class ProgramSession
{
    public string Id { get; set; } = null!;

    public string WeekId { get; set; } = null!;

    public int SessionNumber { get; set; }

    public string Title { get; set; } = null!;

    public string TitleEn { get; set; } = null!;

    public string TitleEs { get; set; } = null!;

    public string TitlePt { get; set; } = null!;

    public string TitleRu { get; set; } = null!;

    public string TitleZhCn { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string SlugEn { get; set; } = null!;

    public string SlugEs { get; set; } = null!;

    public string SlugPt { get; set; } = null!;

    public string SlugRu { get; set; } = null!;

    public string SlugZhCn { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string DescriptionEn { get; set; } = null!;

    public string DescriptionEs { get; set; } = null!;

    public string DescriptionPt { get; set; } = null!;

    public string DescriptionRu { get; set; } = null!;

    public string DescriptionZhCn { get; set; } = null!;

    public List<ExerciseAttributeValueEnum>? Equipment { get; set; } = new List<ExerciseAttributeValueEnum>();

    public int EstimatedMinutes { get; set; }

    public bool IsPremium { get; set; } = true;

    public virtual ProgramWeek Week { get; set; } = null!;

    public virtual ICollection<ProgramSessionExercise> Exercises { get; set; } = new List<ProgramSessionExercise>();

    public virtual ICollection<UserSessionProgress> UserProgress { get; set; } = new List<UserSessionProgress>();
}
