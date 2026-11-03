package formbricks.models

enum class FeedbackSourceStatus(
    val dbName: String,
) {
    ACTIVE("active"),
    PAUSED("paused"),
    ERROR("error"),
}
