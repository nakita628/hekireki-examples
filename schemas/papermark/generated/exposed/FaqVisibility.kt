package papermark.models

enum class FaqVisibility(
    val dbName: String,
) {
    PUBLIC_DATAROOM("PUBLIC_DATAROOM"),
    PUBLIC_LINK("PUBLIC_LINK"),
    PUBLIC_DOCUMENT("PUBLIC_DOCUMENT"),
}
