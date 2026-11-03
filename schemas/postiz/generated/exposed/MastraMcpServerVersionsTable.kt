package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraMcpServerVersionsTable : IdTable<String>("mastra_mcp_server_versions") {
    override val id = text("id").entityId()
    val mcpServerId = text("mcpServerId")
    val versionNumber = integer("versionNumber")
    val name = text("name")
    val version = text("version")
    val description = text("description").nullable()
    val instructions = text("instructions").nullable()
    val repository = jsonb<String>("repository", { it }, { it }).nullable()
    val releaseDate = text("releaseDate").nullable()
    val isLatest = bool("isLatest").nullable()
    val packageCanonical = text("packageCanonical").nullable()
    val tools = jsonb<String>("tools", { it }, { it }).nullable()
    val agents = jsonb<String>("agents", { it }, { it }).nullable()
    val workflows = jsonb<String>("workflows", { it }, { it }).nullable()
    val changedFields = jsonb<String>("changedFields", { it }, { it }).nullable()
    val changeMessage = text("changeMessage").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_mcp_server_versions_pkey")

    init {
        uniqueIndex("idx_mcp_server_versions_server_version", mcpServerId, versionNumber)
    }
}
