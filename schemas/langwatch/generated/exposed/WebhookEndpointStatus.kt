package langwatch.models

enum class WebhookEndpointStatus(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    DISABLED("DISABLED"),
}
