package langwatch.models

enum class SlackIntegrationKind(
    val dbName: String,
) {
    BOT("BOT"),
    INCOMING_WEBHOOK("INCOMING_WEBHOOK"),
}
