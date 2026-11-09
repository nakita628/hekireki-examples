package inboxzero.models

enum class MailSetupState(
    val dbName: String,
) {
    PENDING("PENDING"),
    SUGGESTED("SUGGESTED"),
    DONE("DONE"),
}
