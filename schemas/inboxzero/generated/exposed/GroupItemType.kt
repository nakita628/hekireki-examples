package inboxzero.models

enum class GroupItemType(
    val dbName: String,
) {
    FROM("FROM"),
    SUBJECT("SUBJECT"),
    BODY("BODY"),
}
