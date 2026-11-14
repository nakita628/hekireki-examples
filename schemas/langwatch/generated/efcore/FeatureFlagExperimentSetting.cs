#nullable enable

using System;

namespace LangWatch.Models;

public partial class FeatureFlagExperimentSetting
{
    public string FlagKey { get; set; } = null!;

    public FeatureFlagExperimentSubjectType SubjectType { get; set; }

    public string SubjectId { get; set; } = null!;

    public bool Enabled { get; set; }

    public string? ChangedByUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
