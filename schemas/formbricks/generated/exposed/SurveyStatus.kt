package formbricks.models

enum class SurveyStatus(
    val dbName: String,
) {
    DRAFT("draft"),
    IN_PROGRESS("inProgress"),
    PAUSED("paused"),
    COMPLETED("completed"),
}
