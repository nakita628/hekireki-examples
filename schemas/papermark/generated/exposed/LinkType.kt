package papermark.models

enum class LinkType(
    val dbName: String,
) {
    DOCUMENT_LINK("DOCUMENT_LINK"),
    DATAROOM_LINK("DATAROOM_LINK"),
    WORKFLOW_LINK("WORKFLOW_LINK"),
}
