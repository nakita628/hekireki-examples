#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class NurturingOrganization
{
    public string OrganizationId { get; set; } = null!;

    public string? AdminUserId { get; set; }

    public bool Seeded { get; set; } = false;

    public int EvaluationCount { get; set; } = 0;

    public int SimulationRunCount { get; set; } = 0;

    public DateTime? CodingFirstSessionAt { get; set; }

    public List<string>? CodingAgentTypes { get; set; } = new List<string>();

    public int CodingActiveDays { get; set; } = 0;

    public string? CodingLastActiveDay { get; set; }

    public List<string>? CodingTerminalTypes { get; set; } = new List<string>();

    public List<string>? CodingRepositoryKeys { get; set; } = new List<string>();

    public List<int>? CodingFirstDays { get; set; } = new List<int>();

    public DateTime? LastTraceAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
