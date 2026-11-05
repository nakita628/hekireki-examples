package inboxzero.models

enum class EmailSendOperationStatus(
    val dbName: String,
) {
    PROCESSING("PROCESSING"),
    SENT("SENT"),
    UNCERTAIN("UNCERTAIN"),
}
