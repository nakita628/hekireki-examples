package typebot.models

enum class WorkspaceRole(
    val dbName: String,
) {
    ADMIN("ADMIN"),
    MEMBER("MEMBER"),
    GUEST("GUEST"),
}
