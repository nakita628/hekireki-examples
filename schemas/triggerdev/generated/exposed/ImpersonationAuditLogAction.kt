package triggerdev.models

enum class ImpersonationAuditLogAction(
    val dbName: String,
) {
    START("START"),
    STOP("STOP"),
}
