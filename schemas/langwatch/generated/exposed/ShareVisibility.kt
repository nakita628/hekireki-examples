package langwatch.models

enum class ShareVisibility(
    val dbName: String,
) {
    PUBLIC("PUBLIC"),
    ORGANIZATION("ORGANIZATION"),
    PROJECT("PROJECT"),
}
