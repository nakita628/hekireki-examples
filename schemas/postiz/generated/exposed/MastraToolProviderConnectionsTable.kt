package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraToolProviderConnectionsTable : CompositeIdTable("mastra_tool_provider_connections") {
    val authorId = text("authorId").entityId()
    val providerId = text("providerId").entityId()
    val connectionId = text("connectionId").entityId()
    val toolkit = text("toolkit")
    val label = text("label").nullable()
    val scope = text("scope")
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(authorId, providerId, connectionId, name = "mastra_tool_provider_connections_pkey")

    init {
        index("idx_tool_provider_connections_author", false, authorId, providerId, toolkit)
    }
}
