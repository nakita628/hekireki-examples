package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkflowTable : IdTable<String>("\"Workflow\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val icon = text("icon")
    val description = text("description")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val latestVersionId = text("latestVersionId").nullable()
    val currentVersionId = text("currentVersionId").nullable()
    val publishedId = text("publishedId").nullable()
    val publishedById = text("publishedById").nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val isEvaluator = bool("isEvaluator").default(false)
    val isComponent = bool("isComponent").default(false)
    val copiedFromWorkflowId = text("copiedFromWorkflowId").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Workflow_pkey\"")

    init {
        index("\"Workflow_projectId_idx\"", false, projectId)
        index("\"Workflow_publishedById_idx\"", false, publishedById)
        index("\"Workflow_latestVersionId_idx\"", false, latestVersionId)
        index("\"Workflow_currentVersionId_idx\"", false, currentVersionId)
        index("\"Workflow_copiedFromWorkflowId_idx\"", false, copiedFromWorkflowId)
    }
}
