package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMGuardrailsTableTable : IdTable<String>("\"LiteLLM_GuardrailsTable\"") {
    override val id = text("guardrail_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val guardrailName = text("guardrail_name").uniqueIndex("\"LiteLLM_GuardrailsTable_guardrail_name_key\"")
    val litellmParams = jsonb<String>("litellm_params", { it }, { it })
    val guardrailInfo = jsonb<String>("guardrail_info", { it }, { it }).nullable()
    val teamId = text("team_id").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status = text("status").default("active")
    val submittedAt = pgTimestamp("submitted_at", 3).nullable()
    val reviewedAt = pgTimestamp("reviewed_at", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_GuardrailsTable_pkey\"")

    init {
        index("\"LiteLLM_GuardrailsTable_status_idx\"", false, status)
    }
}
