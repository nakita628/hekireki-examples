package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMWorkflowMessageTable : IdTable<String>("\"LiteLLM_WorkflowMessage\"") {
    override val id = text("message_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val runId =
        reference(
            "run_id",
            LiteLLMWorkflowRunTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_WorkflowMessage_run_id_fkey\"",
        )
    val role = text("role")
    val content = text("content")
    val sequenceNumber = integer("sequence_number")
    val sessionId = text("session_id").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_WorkflowMessage_pkey\"")

    init {
        index("\"LiteLLM_WorkflowMessage_run_id_idx\"", false, runId)
        uniqueIndex("\"LiteLLM_WorkflowMessage_run_id_sequence_number_key\"", runId, sequenceNumber)
    }
}
