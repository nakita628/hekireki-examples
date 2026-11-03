package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraKnowledgeNodesTable : IdTable<String>("mastra_knowledge_nodes") {
    override val id = text("id").entityId()
    val type = text("type")
    val name = text("name")
    val canonicalName = text("canonicalName")
    val kind = text("kind").nullable()
    val content = text("content").nullable()
    val description = text("description").nullable()
    val scope = jsonb<String>("scope", { it }, { it })
    val scopeKey = text("scopeKey")
    val version = integer("version")
    val mergedInto = text("mergedInto").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_knowledge_nodes_pkey")

    init {
        index("idx_knowledge_nodes_scope", false, scopeKey, type)
        uniqueIndex("idx_knowledge_nodes_identity", type, scopeKey, canonicalName)
    }
}
