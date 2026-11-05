package inboxzero.models

enum class CleanAction(
    val dbName: String,
) {
    ARCHIVE("ARCHIVE"),
    MARK_READ("MARK_READ"),
}
