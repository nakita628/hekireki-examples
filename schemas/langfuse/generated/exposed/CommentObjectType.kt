package langfuse.models

enum class CommentObjectType(
    val dbName: String,
) {
    TRACE("TRACE"),
    OBSERVATION("OBSERVATION"),
    SESSION("SESSION"),
    PROMPT("PROMPT"),
}
