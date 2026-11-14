package langwatch.models

enum class ProcessManagerOutboxStatus(
    val dbName: String,
) {
    PENDING("pending"),
    DISPATCHED("dispatched"),
    DEAD("dead"),
    DISCARDED("discarded"),
}
