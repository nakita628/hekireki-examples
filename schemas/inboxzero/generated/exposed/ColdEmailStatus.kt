package inboxzero.models

enum class ColdEmailStatus(
    val dbName: String,
) {
    AI_LABELED_COLD("AI_LABELED_COLD"),
    USER_REJECTED_COLD("USER_REJECTED_COLD"),
}
