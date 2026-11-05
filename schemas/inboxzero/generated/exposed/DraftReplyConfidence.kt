package inboxzero.models

enum class DraftReplyConfidence(
    val dbName: String,
) {
    ALL_EMAILS("ALL_EMAILS"),
    STANDARD("STANDARD"),
    HIGH_CONFIDENCE("HIGH_CONFIDENCE"),
}
