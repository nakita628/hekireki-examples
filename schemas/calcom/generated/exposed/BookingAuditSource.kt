package calcom.models

enum class BookingAuditSource(
    val dbName: String,
) {
    API_V1("api_v1"),
    API_V2("api_v2"),
    WEBAPP("webapp"),
    WEBHOOK("webhook"),
    SYSTEM("system"),
    MAGIC_LINK("magic_link"),
    UNKNOWN("unknown"),
}
