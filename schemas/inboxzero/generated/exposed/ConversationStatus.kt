package inboxzero.models

enum class ConversationStatus(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    STOPPED("STOPPED"),
}
