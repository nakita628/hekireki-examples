package hoppscotch.models

enum class WorkspaceType(
    val dbName: String,
) {
    USER("USER"),
    TEAM("TEAM"),
}
