package inboxzero.models

enum class Frequency(
    val dbName: String,
) {
    NEVER("NEVER"),
    DAILY("DAILY"),
    WEEKLY("WEEKLY"),
}
