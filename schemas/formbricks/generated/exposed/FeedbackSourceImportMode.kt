package formbricks.models

enum class FeedbackSourceImportMode(
    val dbName: String,
) {
    COMPLETED_ONLY("completedOnly"),
    ALL("all"),
}
