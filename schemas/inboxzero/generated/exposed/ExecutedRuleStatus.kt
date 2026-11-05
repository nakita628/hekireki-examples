package inboxzero.models

enum class ExecutedRuleStatus(
    val dbName: String,
) {
    APPLIED("APPLIED"),
    APPLYING("APPLYING"),
    REJECTED("REJECTED"),
    PENDING("PENDING"),
    SKIPPED("SKIPPED"),
    ERROR("ERROR"),
}
