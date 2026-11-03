package triggerdev.models

enum class TaskEventStatus(
    val dbName: String,
) {
    UNSET("UNSET"),
    OK("OK"),
    ERROR("ERROR"),
    UNRECOGNIZED("UNRECOGNIZED"),
}
