package inboxzero.models

enum class CategoryFilterType(
    val dbName: String,
) {
    INCLUDE("INCLUDE"),
    EXCLUDE("EXCLUDE"),
}
