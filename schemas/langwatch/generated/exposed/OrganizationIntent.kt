package langwatch.models

enum class OrganizationIntent(
    val dbName: String,
) {
    AGENT_GOVERNANCE("AGENT_GOVERNANCE"),
    LLM_OPS("LLM_OPS"),
}
