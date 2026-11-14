package langwatch.models

enum class ProcessManagerOutboxAttemptOutcome(
    val dbName: String,
) {
    RETRY_SCHEDULED("retry_scheduled"),
    DEAD("dead"),
}
