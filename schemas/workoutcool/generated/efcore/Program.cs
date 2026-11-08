#nullable enable

using System;
using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class Program
{
    public string Id { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string SlugEn { get; set; } = null!;

    public string SlugEs { get; set; } = null!;

    public string SlugPt { get; set; } = null!;

    public string SlugRu { get; set; } = null!;

    public string SlugZhCn { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string TitleEn { get; set; } = null!;

    public string TitleEs { get; set; } = null!;

    public string TitlePt { get; set; } = null!;

    public string TitleRu { get; set; } = null!;

    public string TitleZhCn { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string DescriptionEn { get; set; } = null!;

    public string DescriptionEs { get; set; } = null!;

    public string DescriptionPt { get; set; } = null!;

    public string DescriptionRu { get; set; } = null!;

    public string DescriptionZhCn { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Image { get; set; } = null!;

    public ProgramLevel Level { get; set; }

    public ExerciseAttributeValueEnum Type { get; set; }

    public int DurationWeeks { get; set; } = 4;

    public int SessionsPerWeek { get; set; } = 3;

    public int SessionDurationMin { get; set; } = 30;

    public List<ExerciseAttributeValueEnum>? Equipment { get; set; } = new List<ExerciseAttributeValueEnum>();

    public bool IsPremium { get; set; } = true;

    public bool IsActive { get; set; } = true;

    public ProgramVisibility Visibility { get; set; } = ProgramVisibility.Draft;

    public int ParticipantCount { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ProgramWeek> Weeks { get; set; } = new List<ProgramWeek>();

    public virtual ICollection<UserProgramEnrollment> Enrollments { get; set; } = new List<UserProgramEnrollment>();

    public virtual ICollection<ProgramCoach> Coaches { get; set; } = new List<ProgramCoach>();
}
