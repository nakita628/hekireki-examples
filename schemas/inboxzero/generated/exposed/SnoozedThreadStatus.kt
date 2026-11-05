package inboxzero.models

enum class SnoozedThreadStatus(
    val dbName: String,
) {
    PREPARING("PREPARING"),
    PENDING("PENDING"),
    EXECUTING("EXECUTING"),
    COMPLETED("COMPLETED"),
    CANCELLED("CANCELLED"),
}
