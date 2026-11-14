package langwatch.models

enum class GatewayGuardrailFailureMode(
    val dbName: String,
) {
    FAIL_OPEN("FAIL_OPEN"),
    FAIL_CLOSED("FAIL_CLOSED"),
}
