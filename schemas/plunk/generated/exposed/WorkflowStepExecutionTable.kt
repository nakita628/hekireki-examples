package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WorkflowStepExecutionTable : IdTable<String>("workflow_step_executions") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val executionId =
        reference(
            "executionId",
            WorkflowExecutionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workflow_step_executions_executionId_fkey\"",
        )
    val stepId =
        reference(
            "stepId",
            WorkflowStepTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workflow_step_executions_stepId_fkey\"",
        )
    val status =
        pgEnum("status", "\"StepExecutionStatus\"", StepExecutionStatus.entries, StepExecutionStatus::dbName).default(
            StepExecutionStatus.PENDING,
        )
    val scheduledFor = pgTimestamp("scheduledFor", 3).nullable()
    val executeAfter = pgTimestamp("executeAfter", 3).nullable()
    val output = jsonb<String>("output", { it }, { it }).nullable()
    val error = text("error").nullable()
    val startedAt = pgTimestamp("startedAt", 3).nullable()
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "workflow_step_executions_pkey")

    init {
        index("\"workflow_step_executions_executionId_status_idx\"", false, executionId, status)
        index("\"workflow_step_executions_stepId_idx\"", false, stepId)
        index("\"workflow_step_executions_status_scheduledFor_idx\"", false, status, scheduledFor)
        index("\"workflow_step_executions_scheduledFor_idx\"", false, scheduledFor)
    }
}
