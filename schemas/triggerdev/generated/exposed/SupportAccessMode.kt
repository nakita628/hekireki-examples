package triggerdev.models

enum class SupportAccessMode(
    val dbName: String,
) {
    ALLOW("ALLOW"),
    REQUIRES_REQUEST("REQUIRES_REQUEST"),
}
