package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMDailyToolSpendTable : CompositeIdTable("\"LiteLLM_DailyToolSpend\"") {
    val date = text("date").entityId()
    val toolName = text("tool_name").entityId()
    val spend = double("spend").default(0.0)
    val totalTokens = long("total_tokens").default(0L)
    val requestCount = long("request_count").default(0L)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(date, toolName, name = "\"LiteLLM_DailyToolSpend_pkey\"")
}
