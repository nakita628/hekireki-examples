package calcom.models

enum class CancellationReasonRequirement(
    val dbName: String,
) {
    MANDATORY_BOTH("MANDATORY_BOTH"),
    MANDATORY_HOST_ONLY("MANDATORY_HOST_ONLY"),
    MANDATORY_ATTENDEE_ONLY("MANDATORY_ATTENDEE_ONLY"),
    OPTIONAL_BOTH("OPTIONAL_BOTH"),
}
