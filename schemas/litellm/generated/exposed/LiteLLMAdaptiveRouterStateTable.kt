package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMAdaptiveRouterStateTable : CompositeIdTable("\"LiteLLM_AdaptiveRouterState\"") {
    val routerName = text("router_name").entityId()
    val requestType = text("request_type").entityId()
    val modelName = text("model_name").entityId()
    val alpha = double("alpha")
    val beta = double("beta")
    val totalSamples = integer("total_samples").default(0)
    val lastUpdatedAt =
        pgTimestamp("last_updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(routerName, requestType, modelName, name = "\"LiteLLM_AdaptiveRouterState_pkey\"")
}
