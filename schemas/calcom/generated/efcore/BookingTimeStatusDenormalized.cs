#nullable enable

using System;

namespace Calcom.Models;

public partial class BookingTimeStatusDenormalized
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int? EventTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Location { get; set; }

    public bool Paid { get; set; }

    public BookingStatus Status { get; set; }

    public bool? Rescheduled { get; set; }

    public int? UserId { get; set; }

    public int? TeamId { get; set; }

    public int? EventLength { get; set; }

    public int? EventParentId { get; set; }

    public string? UserEmail { get; set; }

    public string? UserName { get; set; }

    public string? UserUsername { get; set; }

    public string? RatingFeedback { get; set; }

    public int? Rating { get; set; }

    public bool? NoShowHost { get; set; }

    public bool IsTeamBooking { get; set; }

    public string? TimeStatus { get; set; }
}
