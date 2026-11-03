package formbricks.models

enum class PipelineTriggers(
    val dbName: String,
) {
    RESPONSE_CREATED("responseCreated"),
    RESPONSE_UPDATED("responseUpdated"),
    RESPONSE_FINISHED("responseFinished"),
}
