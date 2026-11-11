package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMRetiredAgentTable : IdTable<String>("\"LiteLLM_RetiredAgent\"") {
    override val id = text("original_agent_id").entityId()
    val retiredAt = pgTimestamp("retired_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_RetiredAgent_pkey\"")
}
