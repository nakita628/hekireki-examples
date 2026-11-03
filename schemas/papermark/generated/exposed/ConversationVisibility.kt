package papermark.models

enum class ConversationVisibility(
    val dbName: String,
) {
    PRIVATE("PRIVATE"),
    PUBLIC_LINK("PUBLIC_LINK"),
    PUBLIC_GROUP("PUBLIC_GROUP"),
    PUBLIC_DOCUMENT("PUBLIC_DOCUMENT"),
    PUBLIC_DATAROOM("PUBLIC_DATAROOM"),
}
