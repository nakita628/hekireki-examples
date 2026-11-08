package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WorkflowExecutionTable : IdTable<String>("workflow_executions") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val workflowId =
        reference(
            "workflowId",
            WorkflowTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workflow_executions_workflowId_fkey\"",
        )
    val contactId =
        reference(
            "contactId",
            ContactTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workflow_executions_contactId_fkey\"",
        )
    val status =
        pgEnum("status", "\"WorkflowExecutionStatus\"", WorkflowExecutionStatus.entries, WorkflowExecutionStatus::dbName).default(
            WorkflowExecutionStatus.RUNNING,
        )
    val currentStepId =
        optReference(
            "currentStepId",
            WorkflowStepTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workflow_executions_currentStepId_fkey\"",
        )
    val exitReason = text("exitReason").nullable()
    val context = jsonb<String>("context", { it }, { it }).nullable()
    val startedAt = pgTimestamp("startedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "workflow_executions_pkey")

    init {
        index("\"workflow_executions_workflowId_contactId_idx\"", false, workflowId, contactId)
        index("\"workflow_executions_workflowId_status_idx\"", false, workflowId, status)
        index("\"workflow_executions_contactId_status_idx\"", false, contactId, status)
        index("\"workflow_executions_status_currentStepId_idx\"", false, status, currentStepId)
    }
}
