package langwatch.models

enum class ModelProviderScopeType(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    TEAM("TEAM"),
    PROJECT("PROJECT"),
}
