package inboxzero.models

enum class McpAuthType(
    val dbName: String,
) {
    OAUTH("OAUTH"),
    API_TOKEN("API_TOKEN"),
    NONE("NONE"),
}
