#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Subscription
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public PlanTypes Plan { get; set; }

    public string? StripeSubscriptionId { get; set; }

    public SubscriptionStatus Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? LastPaymentFailedDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? MaxMembers { get; set; }

    public int? MaxProjects { get; set; }

    public int? MaxMessagesPerMonth { get; set; }

    public int? MaxWorkflows { get; set; }

    public int? MaxRetentionDays { get; set; }

    public int? MaxMembersLite { get; set; }

    public int? MaxMembersDeveloper { get; set; }

    public int? MaxTeams { get; set; }

    public int? MaxPrompts { get; set; }

    public int? MaxEvaluators { get; set; }

    public int? MaxScenarios { get; set; }

    public int? MaxAgents { get; set; }

    public int? MaxExperiments { get; set; }

    public int? MaxOnlineEvaluations { get; set; }

    public int? MaxDatasets { get; set; }

    public int? MaxDashboards { get; set; }

    public int? MaxCustomGraphs { get; set; }

    public int? MaxAutomations { get; set; }

    public int? SeatsKept { get; set; }

    public DateTime? SeatsKeptUntil { get; set; }

    public int? SeatLineQuantity { get; set; }

    public DateTime? SeatLineQuantityAt { get; set; }

    public List<int>? SeatLineWriteQuantities { get; set; } = new List<int>();

    public DateTime? SeatLineWriteSince { get; set; }

    public DateTime? SeatLineWriteAt { get; set; }

    public int? SeatLineWriteAttempts { get; set; }

    public DateTime? SeatLineWriteFlaggedAt { get; set; }

    public int? SeatRepairQuantity { get; set; }

    public int? SeatRepairLiftedQuantity { get; set; }

    public DateTime? SeatRepairSince { get; set; }

    public int? SeatRepairAttempts { get; set; }

    public DateTime? SeatRepairFlaggedAt { get; set; }

    public string? SeatLineLeaseToken { get; set; }

    public DateTime? SeatLineLeaseUntil { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
