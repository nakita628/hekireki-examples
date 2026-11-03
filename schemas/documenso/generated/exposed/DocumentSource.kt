package documenso.models

enum class DocumentSource(
    val dbName: String,
) {
    DOCUMENT("DOCUMENT"),
    TEMPLATE("TEMPLATE"),
    TEMPLATE_DIRECT_LINK("TEMPLATE_DIRECT_LINK"),
}
