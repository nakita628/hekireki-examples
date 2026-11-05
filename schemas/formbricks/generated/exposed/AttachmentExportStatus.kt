package formbricks.models

enum class AttachmentExportStatus(
    val dbName: String,
) {
    QUEUED("queued"),
    RUNNING("running"),
    COMPLETED("completed"),
    FAILED("failed"),
    EXPIRED("expired"),
}
