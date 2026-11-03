package calcom.models

enum class SchedulingType(
    val dbName: String,
) {
    ROUND_ROBIN("roundRobin"),
    COLLECTIVE("collective"),
    MANAGED("managed"),
}
