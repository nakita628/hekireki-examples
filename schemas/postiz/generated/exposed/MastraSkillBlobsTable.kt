package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraSkillBlobsTable : IdTable<String>("mastra_skill_blobs") {
    override val id = text("hash").entityId()
    val content = text("content")
    val size = integer("size")
    val mimeType = text("mimeType").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_skill_blobs_pkey")
}
