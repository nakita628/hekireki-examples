package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMSkillsTableTable : IdTable<String>("\"LiteLLM_SkillsTable\"") {
    override val id = text("skill_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val displayTitle = text("display_title").nullable()
    val description = text("description").nullable()
    val instructions = text("instructions").nullable()
    val source1 = text("source").default("custom")
    val latestVersion = text("latest_version").nullable()
    val fileContent = binary("file_content").nullable()
    val fileName = text("file_name").nullable()
    val fileType = text("file_type").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable().default("{}")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_SkillsTable_pkey\"")
}
