package langfuse.models

enum class SessionViewMode(
    val dbName: String,
) {
    CONVERSATIONAL("conversational"),
    LIST("list"),
}
