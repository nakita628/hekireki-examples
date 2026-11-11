package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMBudgetWindowSpendTable : CompositeIdTable("\"LiteLLM_BudgetWindowSpend\"") {
    val entityType = text("entity_type").entityId()
    val entityId = text("entity_id").entityId()
    val windowDuration = text("window_duration").entityId()
    val windowStart = pgTimestamp("window_start", 3)
    val spend = double("spend").default(0.0)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(entityType, entityId, windowDuration, name = "\"LiteLLM_BudgetWindowSpend_pkey\"")
}
