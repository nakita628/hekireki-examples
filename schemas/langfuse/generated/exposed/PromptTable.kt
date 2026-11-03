package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PromptTable : IdTable<String>("prompts") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "prompts_project_id_fkey",
        )
    val createdBy = text("created_by")
    val prompt = jsonb<String>("prompt", { it }, { it })
    val name = text("name")
    val version = integer("version")
    val type = text("type").default("text")
    val isActive = bool("is_active").nullable()
    val config = json<String>("config", { it }, { it }).default("{}")
    val tags = array("tags", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val labels = array("labels", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val commitMessage = text("commit_message").nullable()
    override val primaryKey = PrimaryKey(id, name = "prompts_pkey")

    init {
        index("prompts_project_id_id_idx", false, projectId, id)
        index("prompts_created_at_idx", false, createdAt)
        index("prompts_updated_at_idx", false, updatedAt)
        index("prompts_tags_idx", false, functions = listOf(indexExpression("\"tags\" array_ops")), indexType = "gin")
        uniqueIndex("prompts_project_id_name_version_key", projectId, name, version)
    }
}
