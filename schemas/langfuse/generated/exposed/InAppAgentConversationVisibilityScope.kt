package langfuse.models

enum class InAppAgentConversationVisibilityScope(
    val dbName: String,
) {
    PERSONAL("PERSONAL"),
    PROJECT("PROJECT"),
}
