package calcom.models

enum class BookingStatus(
    val dbName: String,
) {
    CANCELLED("cancelled"),
    ACCEPTED("accepted"),
    REJECTED("rejected"),
    PENDING("pending"),
    AWAITING_HOST("awaiting_host"),
}
