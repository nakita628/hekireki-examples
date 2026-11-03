package documenso.models

enum class TemplateType(
    val dbName: String,
) {
    PUBLIC("PUBLIC"),
    PRIVATE("PRIVATE"),
    ORGANISATION("ORGANISATION"),
}
