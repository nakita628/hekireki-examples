package formbricks.models

enum class ResponseQuotaLinkStatus(
    val dbName: String,
) {
    SCREENED_IN("screenedIn"),
    SCREENED_OUT("screenedOut"),
}
