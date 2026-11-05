package inboxzero.models

enum class NewsletterStatus(
    val dbName: String,
) {
    APPROVED("APPROVED"),
    UNSUBSCRIBED("UNSUBSCRIBED"),
    AUTO_ARCHIVED("AUTO_ARCHIVED"),
}
