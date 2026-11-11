package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMHealthCheckTableTable : IdTable<String>("\"LiteLLM_HealthCheckTable\"") {
    override val id = text("health_check_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val modelName = text("model_name")
    val modelId = text("model_id").nullable()
    val status = text("status")
    val healthyCount = integer("healthy_count").default(0)
    val unhealthyCount = integer("unhealthy_count").default(0)
    val errorMessage = text("error_message").nullable()
    val responseTimeMs = double("response_time_ms").nullable()
    val details = jsonb<String>("details", { it }, { it }).nullable()
    val checkedBy = text("checked_by").nullable()
    val checkedAt = pgTimestamp("checked_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_HealthCheckTable_pkey\"")

    init {
        index("\"LiteLLM_HealthCheckTable_model_name_idx\"", false, modelName)
        index("\"LiteLLM_HealthCheckTable_checked_at_idx\"", false, checkedAt)
        index("\"LiteLLM_HealthCheckTable_status_idx\"", false, status)
        index(
            "\"LiteLLM_HealthCheckTable_model_id_model_name_checked_at_idx\"",
            false,
            functions = listOf(indexExpression("\"model_id\""), indexExpression("\"model_name\""), indexExpression("\"checked_at\" DESC")),
        )
    }
}
