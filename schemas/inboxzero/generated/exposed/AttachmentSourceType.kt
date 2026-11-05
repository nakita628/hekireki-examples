package inboxzero.models

enum class AttachmentSourceType(
    val dbName: String,
) {
    FILE("FILE"),
    FOLDER("FOLDER"),
}
