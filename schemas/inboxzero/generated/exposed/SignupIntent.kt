package inboxzero.models

enum class SignupIntent(
    val dbName: String,
) {
    AI_ASSISTANT("AI_ASSISTANT"),
    MAIL_CLIENT("MAIL_CLIENT"),
    BOTH("BOTH"),
}
