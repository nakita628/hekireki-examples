package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMBackgroundResponseSettlementTable : IdTable<String>("\"LiteLLM_BackgroundResponseSettlement\"") {
    override val id = text("response_id").entityId()
    val settledAt = pgTimestamp("settled_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_BackgroundResponseSettlement_pkey\"")
}
