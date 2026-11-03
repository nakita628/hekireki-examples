package formbricks.models

enum class EmbeddedDataSource(
    val dbName: String,
) {
    COMPUTED("computed"),
    INGESTED("ingested"),
    RESERVED("reserved"),
}
