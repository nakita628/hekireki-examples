#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class SsoConnection
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string State { get; set; } = null!;

    public List<string>? ClaimedDomains { get; set; }

    public string DomainClaims { get; set; } = "[]";

    public List<string>? ApprovedDomains { get; set; }

    public List<string>? VerifiedDomains { get; set; }

    public List<string>? LapsedDomains { get; set; }

    public string DomainVerifications { get; set; } = "[]";

    public string? PendingVerification { get; set; }

    public string IdpMetadata { get; set; } = null!;

    public string ArrivalPolicy { get; set; } = "refuse";

    public bool AllowsJit { get; set; } = false;

    public DateTime? ArrivalPolicyDecidedAt { get; set; }

    public string Source { get; set; } = null!;

    public string? TestLoginAccountId { get; set; }

    public string? ReplacesConnectionId { get; set; }

    public SsoConnectionMigrationPhase? MigrationPhase { get; set; }

    public DateTime? GraceStartedAt { get; set; }

    public DateTime? RouteChangedAt { get; set; }

    public DateTime? FinalizationRequestedAt { get; set; }

    public DateTime? FinalizedAt { get; set; }

    public string? Rejection { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? TearDownAfter { get; set; }

    public DateTime OccurredAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public DateTime AcceptedAt { get; set; }

    public string ProjectionVersion { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual SsoConnectionReproofCursor? ReproofCursor { get; set; }
}
