package jitsu.models

enum class WorkspaceRole(
    val dbName: String,
) {
    OWNER("owner"),
    EDITOR("editor"),
    ANALYST("analyst"),
}
