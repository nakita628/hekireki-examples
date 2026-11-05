package inboxzero.models

enum class MeetingJoinRule(
    val dbName: String,
) {
    ALL("ALL"),
    EXTERNAL_ONLY("EXTERNAL_ONLY"),
    HOST_ONLY("HOST_ONLY"),
    OFF("OFF"),
}
