package langwatch.models

enum class SlackIntegrationScopeType(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    PROJECT("PROJECT"),
}
