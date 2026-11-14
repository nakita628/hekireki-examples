package langwatch.models

enum class CostType(
    val dbName: String,
) {
    TRACE_CHECK("TRACE_CHECK"),
    GUARDRAIL("GUARDRAIL"),
    CLUSTERING("CLUSTERING"),
    BATCH_EVALUATION("BATCH_EVALUATION"),
}
