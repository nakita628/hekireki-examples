package inboxzero.models

enum class MessagingProvider(
    val dbName: String,
) {
    SLACK("SLACK"),
    TEAMS("TEAMS"),
    TELEGRAM("TELEGRAM"),
}
