#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Scenario
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Situation { get; set; } = null!;

    public List<string>? Criteria { get; set; }

    public List<string>? Labels { get; set; }

    public string? Parameters { get; set; }

    public string? SimulatorModel { get; set; }

    public string? JudgeModel { get; set; }

    public int? MaxTurns { get; set; }

    public int? MinTurns { get; set; }

    public string? Fields { get; set; }

    public string? CallerVoice { get; set; }

    public string? TestSuiteId { get; set; }

    public virtual SimulationSuite? TestSuite { get; set; }

    public int Version { get; set; } = 1;

    public virtual ICollection<ScenarioVersion> Versions { get; set; } = new List<ScenarioVersion>();

    public string? LastUpdatedById { get; set; }

    public virtual User? LastUpdatedBy { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
