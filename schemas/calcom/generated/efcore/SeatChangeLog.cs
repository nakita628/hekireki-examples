#nullable enable

using System;

namespace Calcom.Models;

public partial class SeatChangeLog
{
    public string Id { get; set; } = null!;

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public SeatChangeType ChangeType { get; set; }

    public int SeatCount { get; set; }

    public int? UserId { get; set; }

    public int? TriggeredBy { get; set; }

    public DateTime ChangeDate { get; set; }

    public string MonthKey { get; set; } = null!;

    public string? OperationId { get; set; }

    public string? ProcessedInProrationId { get; set; }

    public virtual MonthlyProration? Proration { get; set; }

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? TeamBillingId { get; set; }

    public virtual TeamBilling? TeamBilling { get; set; }

    public string? OrganizationBillingId { get; set; }

    public virtual OrganizationBilling? OrganizationBilling { get; set; }
}
