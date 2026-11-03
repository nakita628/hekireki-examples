package formbricks.models

enum class RetentionSurveyCondition(
    val dbName: String,
) {
    NO_RESPONSE("noResponse"),
    NO_CHANGE("noChange"),
    CREATED_BEFORE("createdBefore"),
}
