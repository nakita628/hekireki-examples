package triggerdev.models

enum class WebhookEndpointStatus(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    INACTIVE("INACTIVE"),
    DELETING("DELETING"),
}
