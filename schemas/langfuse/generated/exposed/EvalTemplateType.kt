package langfuse.models

enum class EvalTemplateType(
    val dbName: String,
) {
    LLM_AS_JUDGE("LLM_AS_JUDGE"),
    CODE("CODE"),
    DECISION_MODEL("DECISION_MODEL"),
    FACET("FACET"),
}
