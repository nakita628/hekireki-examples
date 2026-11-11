package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMWorkflowEventTable : IdTable<String>("\"LiteLLM_WorkflowEvent\"") {
    override val id = text("event_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val runId =
        reference(
            "run_id",
            LiteLLMWorkflowRunTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_WorkflowEvent_run_id_fkey\"",
        )
    val eventType = text("event_type")
    val stepName = text("step_name")
    val sequenceNumber = integer("sequence_number")
    val data = jsonb<String>("data", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_WorkflowEvent_pkey\"")

    init {
        index("\"LiteLLM_WorkflowEvent_run_id_idx\"", false, runId)
        uniqueIndex("\"LiteLLM_WorkflowEvent_run_id_sequence_number_key\"", runId, sequenceNumber)
    }
}
