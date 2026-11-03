package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraMessagesTable : IdTable<String>("mastra_messages") {
    override val id = text("id").entityId()
    val threadId = text("thread_id")
    val content = text("content")
    val role = text("role")
    val type = text("type")
    val createdAt = pgTimestamp("createdAt", 6)
    val resourceId = text("resourceId").nullable()
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_messages_pkey")

    init {
        index(
            "public_mastra_messages_thread_id_createdat_idx",
            false,
            functions = listOf(indexExpression("\"thread_id\""), indexExpression("\"createdAt\" DESC")),
        )
        index(
            "mastra_messages_thread_id_createdat_idx",
            false,
            functions = listOf(indexExpression("\"thread_id\""), indexExpression("\"createdAt\" DESC")),
        )
    }
}
