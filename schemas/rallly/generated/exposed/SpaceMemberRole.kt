package rallly.models

enum class SpaceMemberRole(
    val dbName: String,
) {
    ADMIN("ADMIN"),
    MEMBER("MEMBER"),
}
