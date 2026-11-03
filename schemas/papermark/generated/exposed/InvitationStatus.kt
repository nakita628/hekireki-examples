package papermark.models

enum class InvitationStatus(
    val dbName: String,
) {
    SENT("SENT"),
    FAILED("FAILED"),
    BOUNCED("BOUNCED"),
}
