package inboxzero.models

enum class ConversationActivityKind(
    val dbName: String,
) {
    INVITED("INVITED"),
    COMMENT("COMMENT"),
    MENTION("MENTION"),
}
