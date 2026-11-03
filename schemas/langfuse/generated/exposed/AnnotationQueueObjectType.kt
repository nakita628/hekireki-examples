package langfuse.models

enum class AnnotationQueueObjectType(
    val dbName: String,
) {
    TRACE("TRACE"),
    OBSERVATION("OBSERVATION"),
    SESSION("SESSION"),
}
