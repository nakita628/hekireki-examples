package inboxzero.models

enum class MessagingMessageStatus(
    val dbName: String,
) {
    SENT("SENT"),
    DRAFT_SENT("DRAFT_SENT"),
    DRAFT_EDITED("DRAFT_EDITED"),
    DISMISSED("DISMISSED"),
    EXPIRED("EXPIRED"),
    FAILED("FAILED"),
}
