package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object LiteLLMAutoRouterBaselineObservationTable : IdTable<String>("\"LiteLLM_AutoRouterBaselineObservation\"") {
    override val id = text("request_id").entityId()
    val scope = text("scope")
    val startedAt = double("started_at")
    val revision = long("revision")
    val data = text("data")
    val publication = text("publication").nullable()
    val conflicted = bool("conflicted").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_AutoRouterBaselineObservation_pkey\"")

    init {
        index("idx_autorouter_baseline_event_order", false, scope, startedAt, id)
        index("idx_autorouter_baseline_event_revision", false, scope, revision, startedAt)
    }
}
