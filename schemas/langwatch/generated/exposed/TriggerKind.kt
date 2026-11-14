package langwatch.models

enum class TriggerKind(
    val dbName: String,
) {
    AUTOMATION("AUTOMATION"),
    ALERT("ALERT"),
    REPORT("REPORT"),
}
