package triggerdev.models

enum class ProjectAlertStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    SENT("SENT"),
    FAILED("FAILED"),
}
