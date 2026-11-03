package postiz.models

enum class CreationMethod(
    val dbName: String,
) {
    UNKNOWN("UNKNOWN"),
    WEB("WEB"),
    MCP("MCP"),
    API("API"),
    AUTOPOST("AUTOPOST"),
    CLI("CLI"),
}
