package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraMcpClientVersionsTable : IdTable<String>("mastra_mcp_client_versions") {
    override val id = text("id").entityId()
    val mcpClientId = text("mcpClientId")
    val versionNumber = integer("versionNumber")
    val name = text("name")
    val description = text("description").nullable()
    val servers = jsonb<String>("servers", { it }, { it })
    val changedFields = jsonb<String>("changedFields", { it }, { it }).nullable()
    val changeMessage = text("changeMessage").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_mcp_client_versions_pkey")

    init {
        uniqueIndex("idx_mcp_client_versions_client_version", mcpClientId, versionNumber)
    }
}
