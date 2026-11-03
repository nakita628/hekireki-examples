package documenso.models

enum class WebhookCallStatus(
    val dbName: String,
) {
    SUCCESS("SUCCESS"),
    FAILED("FAILED"),
}
