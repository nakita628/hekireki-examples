package triggerdev.models

enum class PlatformNotificationSurface(
    val dbName: String,
) {
    WEBAPP("WEBAPP"),
    CLI("CLI"),
}
