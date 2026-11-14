package langwatch.models

enum class DashboardScope(
    val dbName: String,
) {
    PRIVATE("PRIVATE"),
    PROJECT("PROJECT"),
    ORGANIZATION("ORGANIZATION"),
}
