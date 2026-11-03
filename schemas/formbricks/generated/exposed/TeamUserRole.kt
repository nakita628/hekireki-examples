package formbricks.models

enum class TeamUserRole(
    val dbName: String,
) {
    ADMIN("admin"),
    CONTRIBUTOR("contributor"),
}
