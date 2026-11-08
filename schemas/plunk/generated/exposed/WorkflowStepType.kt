package plunk.models

enum class WorkflowStepType(
    val dbName: String,
) {
    TRIGGER("TRIGGER"),
    SEND_EMAIL("SEND_EMAIL"),
    DELAY("DELAY"),
    WAIT_FOR_EVENT("WAIT_FOR_EVENT"),
    CONDITION("CONDITION"),
    EXIT("EXIT"),
    WEBHOOK("WEBHOOK"),
    UPDATE_CONTACT("UPDATE_CONTACT"),
}
