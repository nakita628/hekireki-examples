package inboxzero.models

enum class MailLayout(
    val dbName: String,
) {
    LIST("LIST"),
    SPLIT("SPLIT"),
}
