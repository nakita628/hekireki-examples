package langwatch.models

enum class ShareResourceType(
    val dbName: String,
) {
    TRACE("TRACE"),
    THREAD("THREAD"),
}
