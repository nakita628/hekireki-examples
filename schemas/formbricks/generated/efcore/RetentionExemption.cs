#nullable enable

using System;

namespace Formbricks.Models;

public partial class RetentionExemption
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public RetentionEntity Entity { get; set; }

    public virtual Survey? Survey { get; set; }

    public string? SurveyId { get; set; }

    public DateTime Until { get; set; }

    public RetentionExemptionSource Source { get; set; } = RetentionExemptionSource.Manual;

    public string? Reason { get; set; }

    public virtual User? CreatedBy { get; set; }

    public string? CreatedById { get; set; }

    public DateTime? RevokedAt { get; set; }

    public virtual User? RevokedBy { get; set; }

    public string? RevokedById { get; set; }
}
