package langwatch.models

enum class WebhookDeliveryChannel(
    val dbName: String,
) {
    PLATFORM("platform"),
    AUTOMATIONS("automations"),
}
