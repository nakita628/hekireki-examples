package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraPromptBlockVersionsTable : IdTable<String>("mastra_prompt_block_versions") {
    override val id = text("id").entityId()
    val blockId = text("blockId")
    val versionNumber = integer("versionNumber")
    val name = text("name")
    val description = text("description").nullable()
    val content = text("content")
    val rules = jsonb<String>("rules", { it }, { it }).nullable()
    val requestContextSchema = jsonb<String>("requestContextSchema", { it }, { it }).nullable()
    val changedFields = jsonb<String>("changedFields", { it }, { it }).nullable()
    val changeMessage = text("changeMessage").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_prompt_block_versions_pkey")

    init {
        uniqueIndex("idx_prompt_block_versions_block_version", blockId, versionNumber)
    }
}
