package langfuse.models

enum class Role(
    val dbName: String,
) {
    OWNER("OWNER"),
    ADMIN("ADMIN"),
    MEMBER("MEMBER"),
    VIEWER("VIEWER"),
    NONE("NONE"),
    AI_GATEWAY("AI_GATEWAY"),
}
