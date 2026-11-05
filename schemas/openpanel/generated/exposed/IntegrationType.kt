package openpanel.models

enum class IntegrationType(
    val dbName: String,
) {
    APP("app"),
    MAIL("mail"),
    CUSTOM("custom"),
}
