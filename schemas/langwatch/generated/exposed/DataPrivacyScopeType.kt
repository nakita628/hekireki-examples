package langwatch.models

enum class DataPrivacyScopeType(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    DEPARTMENT("DEPARTMENT"),
    TEAM("TEAM"),
    PROJECT("PROJECT"),
}
