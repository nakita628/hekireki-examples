package typebot.models

enum class CollaborationType(
    val dbName: String,
) {
    READ("READ"),
    WRITE("WRITE"),
    FULL_ACCESS("FULL_ACCESS"),
}
