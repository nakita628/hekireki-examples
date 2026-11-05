package inboxzero.models

enum class MailSplitFilterKind(
    val dbName: String,
) {
    UNREAD("UNREAD"),
    STARRED("STARRED"),
    LABEL("LABEL"),
    CATEGORY("CATEGORY"),
    FROM("FROM"),
    OLDER_THAN("OLDER_THAN"),
}
