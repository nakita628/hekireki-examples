package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SkillTable : IdTable<String>("skills") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "skills_project_id_fkey",
        )
    val createdBy = text("created_by")
    val name = text("name")
    val description = text("description")
    val frontmatter = jsonb<String>("frontmatter", { it }, { it }).default("{}")
    val version = integer("version")
    val tags = array("tags", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val labels = array("labels", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val commitMessage = text("commit_message").nullable()
    override val primaryKey = PrimaryKey(id, name = "skills_pkey")

    init {
        index("skills_created_at_idx", false, createdAt)
        index("skills_updated_at_idx", false, updatedAt)
        index("skills_tags_idx", false, functions = listOf(indexExpression("\"tags\" array_ops")), indexType = "gin")
        index("skills_labels_idx", false, functions = listOf(indexExpression("\"labels\" array_ops")), indexType = "gin")
        uniqueIndex("skills_project_id_name_version_key", projectId, name, version)
    }
}
