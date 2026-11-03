package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraWorkspaceVersionsTable : IdTable<String>("mastra_workspace_versions") {
    override val id = text("id").entityId()
    val workspaceId = text("workspaceId")
    val versionNumber = integer("versionNumber")
    val name = text("name")
    val description = text("description").nullable()
    val filesystem = jsonb<String>("filesystem", { it }, { it }).nullable()
    val sandbox = jsonb<String>("sandbox", { it }, { it }).nullable()
    val mounts = jsonb<String>("mounts", { it }, { it }).nullable()
    val search = jsonb<String>("search", { it }, { it }).nullable()
    val skills = jsonb<String>("skills", { it }, { it }).nullable()
    val tools = jsonb<String>("tools", { it }, { it }).nullable()
    val autoSync = bool("autoSync").nullable()
    val operationTimeout = integer("operationTimeout").nullable()
    val changedFields = jsonb<String>("changedFields", { it }, { it }).nullable()
    val changeMessage = text("changeMessage").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_workspace_versions_pkey")

    init {
        uniqueIndex("idx_workspace_versions_workspace_version", workspaceId, versionNumber)
    }
}
