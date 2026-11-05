package inboxzero.models

enum class EmailReminderStatus(
    val dbName: String,
) {
    NONE("NONE"),
    PENDING("PENDING"),
    PROCESSING("PROCESSING"),
    COMPLETED("COMPLETED"),
    CANCELLED("CANCELLED"),
}
