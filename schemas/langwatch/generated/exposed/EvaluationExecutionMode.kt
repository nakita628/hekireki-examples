package langwatch.models

enum class EvaluationExecutionMode(
    val dbName: String,
) {
    ON_MESSAGE("ON_MESSAGE"),
    AS_GUARDRAIL("AS_GUARDRAIL"),
    MANUALLY("MANUALLY"),
}
