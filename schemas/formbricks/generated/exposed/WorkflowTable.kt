package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkflowTable : IdTable<String>("\"Workflow\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val description = text("description").nullable()
    val status = pgEnum("status", "\"WorkflowStatus\"", WorkflowStatus.entries, WorkflowStatus::dbName).default(WorkflowStatus.DRAFT)
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Workflow_workspaceId_fkey\"",
        )
    val createdBy =
        optReference(
            "createdBy",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Workflow_createdBy_fkey\"",
        )
    val definition = jsonb<String>("definition", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "\"Workflow_pkey\"")

    init {
        index("\"Workflow_workspaceId_status_idx\"", false, workspaceId, status)
        index("\"Workflow_workspaceId_updated_at_idx\"", false, workspaceId, updatedAt)
        uniqueIndex("\"Workflow_id_workspaceId_key\"", id, workspaceId)
    }
}
