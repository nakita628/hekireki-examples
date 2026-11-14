package langwatch.models

enum class WebhookDeliveryOutcome(
    val dbName: String,
) {
    SUCCESS("success"),
    RETRYABLE("retryable"),
    TERMINAL("terminal"),
    PENDING("pending"),
}
