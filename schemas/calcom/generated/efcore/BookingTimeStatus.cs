#nullable enable

using System;

namespace Calcom.Models;

public partial class BookingTimeStatus
{
    public int Id { get; set; }

    public string? Uid { get; set; }

    public int? EventTypeId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Location { get; set; }

    public bool? Paid { get; set; }

    public BookingStatus? Status { get; set; }

    public bool? Rescheduled { get; set; }

    public int? UserId { get; set; }

    public int? TeamId { get; set; }

    public int? EventLength { get; set; }

    public string? TimeStatus { get; set; }

    public int? EventParentId { get; set; }

    public string? UserEmail { get; set; }

    public string? Username { get; set; }

    public string? RatingFeedback { get; set; }

    public int? Rating { get; set; }

    public bool? NoShowHost { get; set; }

    public bool IsTeamBooking { get; set; }
}
