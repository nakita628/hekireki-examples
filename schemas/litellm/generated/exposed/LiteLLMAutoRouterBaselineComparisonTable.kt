package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMAutoRouterBaselineComparisonTable : IdTable<String>("\"LiteLLM_AutoRouterBaselineComparison\"") {
    override val id = text("scope").entityId()
    val apiKey = text("api_key")
    val sessionId = text("session_id")
    val routerName = text("router_name")
    val initialEquivalent = bool("initial_equivalent")
    val revision = long("revision").default(0L)
    val publishedRevision = long("published_revision").default(0L)
    val history = text("history").nullable()
    val attemptedAt = pgTimestamp("attempted_at", 3).nullable()
    val retired = bool("retired").default(false)
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_AutoRouterBaselineComparison_pkey\"")

    init {
        index("idx_autorouter_baseline_scope", false, apiKey, sessionId, routerName)
        index("idx_autorouter_baseline_updated", false, updatedAt)
    }
}
