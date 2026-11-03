#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class DeletionCleanup
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DeletionCleanupKind Kind { get; set; }

    public string OrganizationId { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public List<string>? TenantIds { get; set; } = new List<string>();

    public List<string>? ResponseIds { get; set; } = new List<string>();

    public List<string>? FileKeys { get; set; } = new List<string>();

    public int Attempts { get; set; } = 0;

    public DateTime NextAttemptAt { get; set; }

    public string? LastError { get; set; }
}
