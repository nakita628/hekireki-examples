package langwatch.models

enum class RoutingPolicyScopeType(
    val dbName: String,
) {
    ORGANIZATION("ORGANIZATION"),
    TEAM("TEAM"),
    PROJECT("PROJECT"),
}
