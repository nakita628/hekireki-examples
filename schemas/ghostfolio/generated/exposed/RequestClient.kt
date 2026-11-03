package ghostfolio.models

enum class RequestClient(
    val dbName: String,
) {
    API("API"),
    MCP("MCP"),
    SELF_HOSTING("SELF_HOSTING"),
    WEB("WEB"),
}
