package plunk.models

enum class WorkflowTriggerType(
    val dbName: String,
) {
    EVENT("EVENT"),
    MANUAL("MANUAL"),
    SCHEDULE("SCHEDULE"),
}
