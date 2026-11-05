package inboxzero.models

enum class ExecutedActionStatus(
    val dbName: String,
) {
    SUCCEEDED("SUCCEEDED"),
    FAILED("FAILED"),
    SKIPPED("SKIPPED"),
}
