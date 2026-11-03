package calcom.models

enum class AssignmentReasonEnum(
    val dbName: String,
) {
    REASSIGNED("REASSIGNED"),
    RR_REASSIGNED("RR_REASSIGNED"),
    REROUTED("REROUTED"),
    SALESFORCE_ASSIGNMENT("SALESFORCE_ASSIGNMENT"),
}
