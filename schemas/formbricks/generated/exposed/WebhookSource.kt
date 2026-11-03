package formbricks.models

enum class WebhookSource(
    val dbName: String,
) {
    USER("user"),
    ZAPIER("zapier"),
    MAKE("make"),
    N8N("n8n"),
    ACTIVEPIECES("activepieces"),
}
