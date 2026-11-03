#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Booking
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public string? IdempotencyKey { get; set; }

    public virtual User? User { get; set; }

    public int? UserId { get; set; }

    public string? UserPrimaryEmail { get; set; }

    public virtual ICollection<BookingReference> References { get; set; } = new List<BookingReference>();

    public virtual EventType? EventType { get; set; }

    public int? EventTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? CustomInputs { get; set; }

    public string? Responses { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual ICollection<Attendee> Attendees { get; set; } = new List<Attendee>();

    public string? Location { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Accepted;

    public bool Paid { get; set; } = false;

    public virtual ICollection<Payment> Payment { get; set; } = new List<Payment>();

    public virtual DestinationCalendar? DestinationCalendar { get; set; }

    public int? DestinationCalendarId { get; set; }

    public string? CancellationReason { get; set; }

    public string? RejectionReason { get; set; }

    public string? ReassignReason { get; set; }

    public virtual User? ReassignBy { get; set; }

    public int? ReassignById { get; set; }

    public string? DynamicEventSlugRef { get; set; }

    public string? DynamicGroupSlugRef { get; set; }

    public bool? Rescheduled { get; set; }

    public string? FromReschedule { get; set; }

    public string? RecurringEventId { get; set; }

    public string? SmsReminderNumber { get; set; }

    public List<string>? ScheduledJobs { get; set; }

    public virtual ICollection<BookingSeat> SeatsReferences { get; set; } = new List<BookingSeat>();

    public string? Metadata { get; set; }

    public bool IsRecorded { get; set; } = false;

    public string? ICalUID { get; set; } = "";

    public int ICalSequence { get; set; } = 0;

    public virtual InstantMeetingToken? InstantMeetingToken { get; set; }

    public int? Rating { get; set; }

    public string? RatingFeedback { get; set; }

    public bool? NoShowHost { get; set; } = false;

    public virtual ICollection<WebhookScheduledTriggers> ScheduledTriggers { get; set; } = new List<WebhookScheduledTriggers>();

    public string? OneTimePassword { get; set; } = Guid.NewGuid().ToString();

    public string? CancelledBy { get; set; }

    public string? RescheduledBy { get; set; }

    public virtual ICollection<AssignmentReason> AssignmentReason { get; set; } = new List<AssignmentReason>();

    public virtual ICollection<BookingInternalNote> InternalNote { get; set; } = new List<BookingInternalNote>();

    public CreationSource? CreationSource { get; set; }

    public virtual Tracking? Tracking { get; set; }

    public virtual ICollection<CreditExpenseLog> ExpenseLogs { get; set; } = new List<CreditExpenseLog>();

    public virtual BookingReport? Report { get; set; }

    public virtual ICollection<WrongAssignmentReport> WrongAssignmentReports { get; set; } = new List<WrongAssignmentReport>();
}
