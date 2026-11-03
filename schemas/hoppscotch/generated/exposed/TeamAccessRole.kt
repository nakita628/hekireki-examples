package hoppscotch.models

enum class TeamAccessRole(
    val dbName: String,
) {
    OWNER("OWNER"),
    VIEWER("VIEWER"),
    EDITOR("EDITOR"),
}
