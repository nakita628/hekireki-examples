package papermark.models

enum class ItemType(
    val dbName: String,
) {
    DATAROOM_DOCUMENT("DATAROOM_DOCUMENT"),
    DATAROOM_FOLDER("DATAROOM_FOLDER"),
}
