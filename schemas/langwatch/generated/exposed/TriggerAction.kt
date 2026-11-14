package langwatch.models

enum class TriggerAction(
    val dbName: String,
) {
    SEND_EMAIL("SEND_EMAIL"),
    ADD_TO_DATASET("ADD_TO_DATASET"),
    ADD_TO_ANNOTATION_QUEUE("ADD_TO_ANNOTATION_QUEUE"),
    SEND_SLACK_MESSAGE("SEND_SLACK_MESSAGE"),
    SEND_WEBHOOK("SEND_WEBHOOK"),
}
