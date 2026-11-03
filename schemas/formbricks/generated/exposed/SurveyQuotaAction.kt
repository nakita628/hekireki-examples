package formbricks.models

enum class SurveyQuotaAction(
    val dbName: String,
) {
    END_SURVEY("endSurvey"),
    CONTINUE_SURVEY("continueSurvey"),
}
