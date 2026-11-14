package langwatch.models

enum class TeamUserRole(
    val dbName: String,
) {
    ADMIN("ADMIN"),
    MEMBER("MEMBER"),
    VIEWER("VIEWER"),
    CUSTOM("CUSTOM"),
}
