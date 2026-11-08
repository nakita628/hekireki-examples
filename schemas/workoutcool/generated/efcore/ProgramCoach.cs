#nullable enable

namespace WorkoutCool.Models;

public partial class ProgramCoach
{
    public string Id { get; set; } = null!;

    public string ProgramId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int Order { get; set; } = 0;

    public virtual Program Program { get; set; } = null!;
}
