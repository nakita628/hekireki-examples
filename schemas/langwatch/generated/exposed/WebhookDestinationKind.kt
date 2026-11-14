package langwatch.models

enum class WebhookDestinationKind(
    val dbName: String,
) {
    HTTP("http"),
    SQS("sqs"),
}
