package triggerdev.models

enum class ScheduleType(
    val dbName: String,
) {
    DECLARATIVE("DECLARATIVE"),
    IMPERATIVE("IMPERATIVE"),
}
