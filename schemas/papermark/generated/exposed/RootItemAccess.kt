package papermark.models

enum class RootItemAccess(
    val dbName: String,
) {
    VIEW_ONLY("VIEW_ONLY"),
    VIEW_AND_DOWNLOAD("VIEW_AND_DOWNLOAD"),
    HIDDEN("HIDDEN"),
}
