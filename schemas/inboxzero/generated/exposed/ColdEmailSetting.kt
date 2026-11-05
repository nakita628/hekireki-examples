package inboxzero.models

enum class ColdEmailSetting(
    val dbName: String,
) {
    DISABLED("DISABLED"),
    LIST("LIST"),
    LABEL("LABEL"),
    ARCHIVE_AND_LABEL("ARCHIVE_AND_LABEL"),
    ARCHIVE_AND_READ_AND_LABEL("ARCHIVE_AND_READ_AND_LABEL"),
}
