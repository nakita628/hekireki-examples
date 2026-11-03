package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkflowVersionTable : IdTable<String>("\"WorkflowVersion\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val workflowId = text("workflowId")
    val workspaceId = text("workspaceId")
    val version = integer("version")
    val definition = jsonb<String>("definition", { it }, { it })
    val publishedAt = pgTimestamp("publishedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val publishedBy =
        optReference(
            "publishedBy",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkflowVersion_publishedBy_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"WorkflowVersion_pkey\"")

    init {
        index("\"WorkflowVersion_workflowId_workspaceId_idx\"", false, workflowId, workspaceId)
        index("\"WorkflowVersion_publishedBy_idx\"", false, publishedBy)
        uniqueIndex("\"WorkflowVersion_workflowId_version_key\"", workflowId, version)
        foreignKey(
            workflowId to WorkflowTable.id,
            workspaceId to WorkflowTable.workspaceId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"WorkflowVersion_workflowId_workspaceId_fkey\"",
        )
    }
}
