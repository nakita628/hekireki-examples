package formbricks.models

enum class IntegrationType(
    val dbName: String,
) {
    GOOGLE_SHEETS("googleSheets"),
    NOTION("notion"),
    AIRTABLE("airtable"),
    SLACK("slack"),
}
