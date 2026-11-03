package formbricks.models

enum class WorkspaceTeamPermission(
    val dbName: String,
) {
    READ("read"),
    READ_WRITE("readWrite"),
    MANAGE("manage"),
}
