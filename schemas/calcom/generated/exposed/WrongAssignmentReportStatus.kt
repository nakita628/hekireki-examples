package calcom.models

enum class WrongAssignmentReportStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    REVIEWED("REVIEWED"),
    RESOLVED("RESOLVED"),
    DISMISSED("DISMISSED"),
}
