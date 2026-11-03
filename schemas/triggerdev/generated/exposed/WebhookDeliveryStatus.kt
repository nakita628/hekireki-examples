package triggerdev.models

enum class WebhookDeliveryStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    PROCESSING("PROCESSING"),
    SUCCEEDED("SUCCEEDED"),
    FAILED("FAILED"),
    FILTERED("FILTERED"),
    UNMATCHED("UNMATCHED"),
}
