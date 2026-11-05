package inboxzero.models

enum class BookingStatus(
    val dbName: String,
) {
    PENDING_PROVIDER_EVENT("PENDING_PROVIDER_EVENT"),
    CONFIRMED("CONFIRMED"),
    CANCELED("CANCELED"),
    FAILED("FAILED"),
}
