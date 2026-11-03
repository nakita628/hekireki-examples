package calcom.models

enum class SystemReportStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    BLOCKED("BLOCKED"),
    DISMISSED("DISMISSED"),
}
