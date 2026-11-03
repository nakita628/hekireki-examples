package calcom.models

enum class BookingAuditAction(
    val dbName: String,
) {
    CREATED("created"),
    CANCELLED("cancelled"),
    ACCEPTED("accepted"),
    REJECTED("rejected"),
    PENDING("pending"),
    AWAITING_HOST("awaiting_host"),
    RESCHEDULED("rescheduled"),
    ATTENDEE_ADDED("attendee_added"),
    ATTENDEE_REMOVED("attendee_removed"),
    REASSIGNMENT("reassignment"),
    LOCATION_CHANGED("location_changed"),
    NO_SHOW_UPDATED("no_show_updated"),
    RESCHEDULE_REQUESTED("reschedule_requested"),
    SEAT_BOOKED("seat_booked"),
    SEAT_RESCHEDULED("seat_rescheduled"),
}
