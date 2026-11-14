package langwatch.models

enum class PinSource(
    val dbName: String,
) {
    MANUAL("manual"),
    SHARE("share"),
}
