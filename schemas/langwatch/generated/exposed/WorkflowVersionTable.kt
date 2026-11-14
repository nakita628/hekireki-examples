package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkflowVersionTable : IdTable<String>("\"WorkflowVersion\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val version = text("version")
    val commitMessage = text("commitMessage")
    val authorId = text("authorId")
    val projectId = text("projectId")
    val workflowId = text("workflowId")
    val parentId = text("parentId").nullable()
    val autoSaved = bool("autoSaved").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val dsl = jsonb<String>("dsl", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "\"WorkflowVersion_pkey\"")

    init {
        index("\"WorkflowVersion_workflowId_idx\"", false, workflowId)
        index("\"WorkflowVersion_projectId_idx\"", false, projectId)
        index("\"WorkflowVersion_parentId_idx\"", false, parentId)
        index("\"WorkflowVersion_authorId_idx\"", false, authorId)
        index("\"WorkflowVersion_createdAt_idx\"", false, createdAt)
        uniqueIndex("\"WorkflowVersion_workflowId_version_key\"", workflowId, version)
    }
}
