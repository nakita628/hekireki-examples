package inboxzero.models

enum class MeetingRecordingStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    SCHEDULED("SCHEDULED"),
    CANCELLING("CANCELLING"),
    JOINING("JOINING"),
    IN_WAITING_ROOM("IN_WAITING_ROOM"),
    IN_CALL("IN_CALL"),
    RECORDING("RECORDING"),
    CALL_ENDED("CALL_ENDED"),
    DONE("DONE"),
    FAILED("FAILED"),
    CANCELLED("CANCELLED"),
}
