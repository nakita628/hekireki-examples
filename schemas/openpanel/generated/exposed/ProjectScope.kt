package openpanel.models

enum class ProjectScope(
    val dbName: String,
) {
    ALL("all"),
    SELECTED("selected"),
}
