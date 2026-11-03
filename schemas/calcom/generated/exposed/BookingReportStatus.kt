package calcom.models

enum class BookingReportStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    DISMISSED("DISMISSED"),
    BLOCKED("BLOCKED"),
}
