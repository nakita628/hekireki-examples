package inboxzero.models

enum class ReplyMemoryKind(
    val dbName: String,
) {
    FACT("FACT"),
    PREFERENCE("PREFERENCE"),
    PROCEDURE("PROCEDURE"),
}
