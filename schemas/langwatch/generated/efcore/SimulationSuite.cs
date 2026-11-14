#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class SimulationSuite
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public List<string>? ScenarioIds { get; set; }

    public string Targets { get; set; } = null!;

    public int RepeatCount { get; set; } = 1;

    public List<string>? Labels { get; set; }

    public string? SimulatorModel { get; set; }

    public string? JudgeModel { get; set; }

    public string Kind { get; set; } = "run_plan";

    public string? Scope { get; set; }

    public string? Fields { get; set; }

    public string? Evaluators { get; set; }

    public virtual ICollection<Scenario> TestSuiteScenarios { get; set; } = new List<Scenario>();

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
