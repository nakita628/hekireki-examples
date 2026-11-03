package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraKnowledgeActivityTable : IdTable<String>("mastra_knowledge_activity") {
    override val id = text("id").entityId()
    val action = text("action")
    val recordType = text("recordType")
    val recordId = text("recordId")
    val scope = jsonb<String>("scope", { it }, { it })
    val scopeKey = text("scopeKey")
    val sourceThreadId = text("sourceThreadId").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_knowledge_activity_pkey")

    init {
        index("idx_knowledge_activity_latest", false, functions = listOf(indexExpression("\"id\" DESC")))
    }
}
