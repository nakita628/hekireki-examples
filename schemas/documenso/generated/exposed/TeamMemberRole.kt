package documenso.models

enum class TeamMemberRole(
    val dbName: String,
) {
    ADMIN("ADMIN"),
    MANAGER("MANAGER"),
    MEMBER("MEMBER"),
}
