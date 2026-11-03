package ghostfolio.models

enum class AccessType(
    val dbName: String,
) {
    MCP("MCP"),
    PRIVATE("PRIVATE"),
    PUBLIC("PUBLIC"),
}
