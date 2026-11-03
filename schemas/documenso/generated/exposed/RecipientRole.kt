package documenso.models

enum class RecipientRole(
    val dbName: String,
) {
    CC("CC"),
    SIGNER("SIGNER"),
    VIEWER("VIEWER"),
    APPROVER("APPROVER"),
    ASSISTANT("ASSISTANT"),
}
