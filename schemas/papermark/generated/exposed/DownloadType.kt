package papermark.models

enum class DownloadType(
    val dbName: String,
) {
    SINGLE("SINGLE"),
    BULK("BULK"),
    FOLDER("FOLDER"),
}
