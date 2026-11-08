#nullable enable

using System.Collections.Generic;

namespace WorkoutCool.Models;

public partial class ProgramSessionExercise
{
    public string Id { get; set; } = null!;

    public string SessionId { get; set; } = null!;

    public string ExerciseId { get; set; } = null!;

    public int Order { get; set; }

    public string Instructions { get; set; } = null!;

    public string InstructionsEn { get; set; } = null!;

    public string InstructionsEs { get; set; } = null!;

    public string InstructionsPt { get; set; } = null!;

    public string InstructionsRu { get; set; } = null!;

    public string InstructionsZhCn { get; set; } = null!;

    public virtual ProgramSession Session { get; set; } = null!;

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual ICollection<ProgramSuggestedSet> SuggestedSets { get; set; } = new List<ProgramSuggestedSet>();
}
