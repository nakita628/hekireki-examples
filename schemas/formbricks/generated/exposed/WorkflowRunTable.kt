package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkflowRunTable : IdTable<String>("\"WorkflowRun\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val workflowId = text("workflowId")
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkflowRun_workspaceId_fkey\"",
        )
    val workflowVersionId =
        optReference(
            "workflowVersionId",
            WorkflowVersionTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkflowRun_workflowVersionId_fkey\"",
        )
    val responseId =
        optReference(
            "responseId",
            ResponseTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkflowRun_responseId_fkey\"",
        )
    val status =
        pgEnum("status", "\"WorkflowRunStatus\"", WorkflowRunStatus.entries, WorkflowRunStatus::dbName).default(WorkflowRunStatus.QUEUED)
    val triggerType = text("triggerType")
    val surveyId = text("surveyId").nullable()
    val isDryRun = bool("isDryRun").default(false)
    val idempotencyKey = text("idempotencyKey").nullable()
    val attempt = integer("attempt").default(0)
    val nextAttemptAt = pgTimestamp("nextAttemptAt", 3).nullable()
    val lastErrorAt = pgTimestamp("lastErrorAt", 3).nullable()
    val triggerPayload = jsonb<String>("triggerPayload", { it }, { it })
    val data = jsonb<String>("data", { it }, { it }).default("{}")
    val error = text("error").nullable()
    val dispatchedAt = pgTimestamp("dispatchedAt", 3).nullable()
    val startedAt = pgTimestamp("startedAt", 3).nullable()
    val finishedAt = pgTimestamp("finishedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"WorkflowRun_pkey\"")

    init {
        index("\"WorkflowRun_workspaceId_created_at_idx\"", false, workspaceId, createdAt)
        index("\"WorkflowRun_workflowId_workspaceId_created_at_idx\"", false, workflowId, workspaceId, createdAt)
        index("\"WorkflowRun_status_created_at_idx\"", false, status, createdAt)
        index("\"WorkflowRun_responseId_idx\"", false, responseId)
        index("\"WorkflowRun_isDryRun_idx\"", false, isDryRun)
        index("\"WorkflowRun_nextAttemptAt_idx\"", false, nextAttemptAt)
        uniqueIndex("\"WorkflowRun_workflowId_idempotencyKey_key\"", workflowId, idempotencyKey)
        foreignKey(
            workflowId to WorkflowTable.id,
            workspaceId to WorkflowTable.workspaceId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"WorkflowRun_workflowId_workspaceId_fkey\"",
        )
    }
}
