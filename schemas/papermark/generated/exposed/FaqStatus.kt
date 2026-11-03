package papermark.models

enum class FaqStatus(
    val dbName: String,
) {
    DRAFT("DRAFT"),
    PUBLISHED("PUBLISHED"),
    ARCHIVED("ARCHIVED"),
}
