package documenso.models

enum class DocumentStatus(
    val dbName: String,
) {
    DRAFT("DRAFT"),
    PENDING("PENDING"),
    COMPLETED("COMPLETED"),
    REJECTED("REJECTED"),
    CANCELLED("CANCELLED"),
}
