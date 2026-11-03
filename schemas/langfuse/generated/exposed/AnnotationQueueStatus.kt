package langfuse.models

enum class AnnotationQueueStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    COMPLETED("COMPLETED"),
}
