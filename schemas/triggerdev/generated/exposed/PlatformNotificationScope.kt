package triggerdev.models

enum class PlatformNotificationScope(
    val dbName: String,
) {
    USER("USER"),
    PROJECT("PROJECT"),
    ORGANIZATION("ORGANIZATION"),
    GLOBAL("GLOBAL"),
}
