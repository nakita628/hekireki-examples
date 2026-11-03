package calcom.models

enum class AuditActorType(
    val dbName: String,
) {
    USER("user"),
    GUEST("guest"),
    ATTENDEE("attendee"),
    SYSTEM("system"),
    APP("app"),
}
