package papermark.models

enum class TagType(
    val dbName: String,
) {
    LINK_TAG("LINK_TAG"),
    DOCUMENT_TAG("DOCUMENT_TAG"),
    DATAROOM_TAG("DATAROOM_TAG"),
}
