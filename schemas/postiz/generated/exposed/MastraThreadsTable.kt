package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraThreadsTable : IdTable<String>("mastra_threads") {
    override val id = text("id").entityId()
    val resourceId = text("resourceId")
    val title = text("title")
    val metadata = text("metadata").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_threads_pkey")

    init {
        index(
            "public_mastra_threads_resourceid_createdat_idx",
            false,
            functions = listOf(indexExpression("\"resourceId\""), indexExpression("\"createdAt\" DESC")),
        )
        index(
            "mastra_threads_resourceid_createdat_idx",
            false,
            functions = listOf(indexExpression("\"resourceId\""), indexExpression("\"createdAt\" DESC")),
        )
    }
}
