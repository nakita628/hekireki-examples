package langwatch.models

enum class RetentionPolicyScopeType(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    TEAM("TEAM"),
    PROJECT("PROJECT"),
}
