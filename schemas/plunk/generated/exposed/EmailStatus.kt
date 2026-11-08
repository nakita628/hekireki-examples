package plunk.models

enum class EmailStatus(
    val dbName: String,
) {
    SCHEDULED("SCHEDULED"),
    CANCELLED("CANCELLED"),
    PENDING("PENDING"),
    SENDING("SENDING"),
    SENT("SENT"),
    DELIVERED("DELIVERED"),
    RECEIVED("RECEIVED"),
    OPENED("OPENED"),
    CLICKED("CLICKED"),
    BOUNCED("BOUNCED"),
    COMPLAINED("COMPLAINED"),
    FAILED("FAILED"),
}
