package formbricks.models

enum class FeedbackSourceType(
    val dbName: String,
) {
    FORMBRICKS_SURVEY("formbricks_survey"),
    CSV("csv"),
}
