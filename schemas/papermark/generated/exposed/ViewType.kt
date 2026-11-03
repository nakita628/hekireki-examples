package papermark.models

enum class ViewType(
    val dbName: String,
) {
    DOCUMENT_VIEW("DOCUMENT_VIEW"),
    DATAROOM_VIEW("DATAROOM_VIEW"),
}
