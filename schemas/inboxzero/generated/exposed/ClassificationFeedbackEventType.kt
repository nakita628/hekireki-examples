package inboxzero.models

enum class ClassificationFeedbackEventType(
    val dbName: String,
) {
    LABEL_ADDED("LABEL_ADDED"),
    LABEL_REMOVED("LABEL_REMOVED"),
}
