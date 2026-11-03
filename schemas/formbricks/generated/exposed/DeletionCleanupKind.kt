package formbricks.models

enum class DeletionCleanupKind(
    val dbName: String,
) {
    HUB_SURVEY("hubSurvey"),
    HUB_RESPONSES("hubResponses"),
    STORAGE_FILES("storageFiles"),
    STORAGE_SURVEY_FOLDER("storageSurveyFolder"),
}
