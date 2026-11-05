package inboxzero.models

enum class ReplyMemoryScopeType(
    val dbName: String,
) {
    GLOBAL("GLOBAL"),
    SENDER("SENDER"),
    DOMAIN("DOMAIN"),
    TOPIC("TOPIC"),
}
