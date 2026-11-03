package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraSkillVersionsTable : IdTable<String>("mastra_skill_versions") {
    override val id = text("id").entityId()
    val skillId = text("skillId")
    val versionNumber = integer("versionNumber")
    val name = text("name")
    val description = text("description")
    val instructions = text("instructions")
    val license = text("license").nullable()
    val compatibility = jsonb<String>("compatibility", { it }, { it }).nullable()
    val source1 = jsonb<String>("source", { it }, { it }).nullable()
    val references = jsonb<String>("references", { it }, { it }).nullable()
    val scripts = jsonb<String>("scripts", { it }, { it }).nullable()
    val assets = jsonb<String>("assets", { it }, { it }).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val tree = jsonb<String>("tree", { it }, { it }).nullable()
    val changedFields = jsonb<String>("changedFields", { it }, { it }).nullable()
    val changeMessage = text("changeMessage").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val files = jsonb<String>("files", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "mastra_skill_versions_pkey")

    init {
        uniqueIndex("idx_skill_versions_skill_version", skillId, versionNumber)
    }
}
