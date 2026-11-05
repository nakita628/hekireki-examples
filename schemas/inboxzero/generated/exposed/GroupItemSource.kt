package inboxzero.models

enum class GroupItemSource(
    val dbName: String,
) {
    AI("AI"),
    USER("USER"),
    LABEL_REMOVED("LABEL_REMOVED"),
    LABEL_ADDED("LABEL_ADDED"),
}
