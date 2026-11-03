#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum BookingAuditAction
{
    [PgName("created")]
    Created,

    [PgName("cancelled")]
    Cancelled,

    [PgName("accepted")]
    Accepted,

    [PgName("rejected")]
    Rejected,

    [PgName("pending")]
    Pending,

    [PgName("awaiting_host")]
    AwaitingHost,

    [PgName("rescheduled")]
    Rescheduled,

    [PgName("attendee_added")]
    AttendeeAdded,

    [PgName("attendee_removed")]
    AttendeeRemoved,

    [PgName("reassignment")]
    Reassignment,

    [PgName("location_changed")]
    LocationChanged,

    [PgName("no_show_updated")]
    NoShowUpdated,

    [PgName("reschedule_requested")]
    RescheduleRequested,

    [PgName("seat_booked")]
    SeatBooked,

    [PgName("seat_rescheduled")]
    SeatRescheduled,
}
