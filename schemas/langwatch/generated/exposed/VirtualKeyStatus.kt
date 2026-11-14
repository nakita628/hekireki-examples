package langwatch.models

enum class VirtualKeyStatus(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    DISABLED("DISABLED"),
    REVOKED("REVOKED"),
}
