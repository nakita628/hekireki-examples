package formbricks.models

enum class CreativeStatus(
    val dbName: String,
) {
    DRAFT("draft"),
    IN_PROGRESS("inProgress"),
    PAUSED("paused"),
    COMPLETED("completed"),
}
