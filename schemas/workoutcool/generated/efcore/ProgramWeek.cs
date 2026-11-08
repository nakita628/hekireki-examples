#nullable enable

using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class ProgramWeek
{
    public string Id { get; set; } = null!;

    public string ProgramId { get; set; } = null!;

    public int WeekNumber { get; set; }

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

    public virtual Program Program { get; set; } = null!;

    public virtual ICollection<ProgramSession> Sessions { get; set; } = new List<ProgramSession>();
}
