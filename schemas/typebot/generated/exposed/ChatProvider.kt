package typebot.models

enum class ChatProvider(
    val dbName: String,
) {
    WHATSAPP("WHATSAPP"),
    DIALOG360("DIALOG360"),
}
