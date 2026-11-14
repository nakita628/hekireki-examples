package langwatch.models

enum class GrantScopeType(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    TEAM("TEAM"),
    PROJECT("PROJECT"),
    RESOURCE("RESOURCE"),
    PLATFORM("PLATFORM"),
}
