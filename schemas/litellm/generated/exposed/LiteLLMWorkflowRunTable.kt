package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMWorkflowRunTable : IdTable<String>("\"LiteLLM_WorkflowRun\"") {
    override val id = text("run_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val sessionId = text("session_id").clientDefault { UUID.randomUUID().toString() }.uniqueIndex("\"LiteLLM_WorkflowRun_session_id_key\"")
    val workflowType = text("workflow_type")
    val status = text("status").default("pending")
    val createdBy = text("created_by").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val input = jsonb<String>("input", { it }, { it }).nullable()
    val output = jsonb<String>("output", { it }, { it }).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_WorkflowRun_pkey\"")

    init {
        index("\"LiteLLM_WorkflowRun_workflow_type_status_idx\"", false, workflowType, status)
        index("\"LiteLLM_WorkflowRun_session_id_idx\"", false, sessionId)
        index("\"LiteLLM_WorkflowRun_created_at_idx\"", false, createdAt)
        index("\"LiteLLM_WorkflowRun_created_by_idx\"", false, createdBy)
    }
}
