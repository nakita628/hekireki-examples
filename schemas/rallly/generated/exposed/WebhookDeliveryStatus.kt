package rallly.models

enum class WebhookDeliveryStatus(
    val dbName: String,
) {
    PENDING("pending"),
    IN_FLIGHT("in_flight"),
    SUCCEEDED("succeeded"),
    FAILED("failed"),
    EXHAUSTED("exhausted"),
}
