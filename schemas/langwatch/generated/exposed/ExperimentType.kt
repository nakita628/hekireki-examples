package langwatch.models

enum class ExperimentType(
    val dbName: String,
) {
    DSPY("DSPY"),
    BATCH_EVALUATION("BATCH_EVALUATION"),
    BATCH_EVALUATION_V2("BATCH_EVALUATION_V2"),
    EVALUATIONS_V3("EVALUATIONS_V3"),
}
