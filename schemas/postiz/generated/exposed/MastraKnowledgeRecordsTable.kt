package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraKnowledgeRecordsTable : IdTable<String>("mastra_knowledge_records") {
    override val id = text("id").entityId()
    val node = text("node")
    val text = text("text")
    val scope = jsonb<String>("scope", { it }, { it })
    val scopeKey = text("scopeKey")
    val sourceThreadId = text("sourceThreadId")
    val capturedAt = pgTimestamp("capturedAt", 6)
    val `when` = pgTimestamp("when", 6).nullable()
    val maxScope = text("maxScope").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val deletedAt = pgTimestamp("deletedAt", 6).nullable()
    val deletedBy = text("deletedBy").nullable()
    val capturedAtZ =
        pgTimestamptz("capturedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val whenZ = pgTimestamptz("whenZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAtZ =
        pgTimestamptz("deletedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_knowledge_records_pkey")

    init {
        index("idx_knowledge_records_node_latest", false, functions = listOf(indexExpression("\"node\""), indexExpression("\"id\" DESC")))
        index(
            "idx_knowledge_records_thread_latest",
            false,
            functions = listOf(indexExpression("\"sourceThreadId\""), indexExpression("\"id\" DESC")),
        )
    }
}
