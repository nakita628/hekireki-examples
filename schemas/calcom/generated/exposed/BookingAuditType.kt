package calcom.models

enum class BookingAuditType(
    val dbName: String,
) {
    RECORD_CREATED("record_created"),
    RECORD_UPDATED("record_updated"),
    RECORD_DELETED("record_deleted"),
}
