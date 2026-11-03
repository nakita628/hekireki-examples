package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PromptDependencyTable : IdTable<String>("prompt_dependencies") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "prompt_dependencies_project_id_fkey",
        )
    val parentId =
        reference(
            "parent_id",
            PromptTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "prompt_dependencies_parent_id_fkey",
        )
    val childName = text("child_name")
    val childLabel = text("child_label").nullable()
    val childVersion = integer("child_version").nullable()
    override val primaryKey = PrimaryKey(id, name = "prompt_dependencies_pkey")

    init {
        index("prompt_dependencies_project_id_parent_id", false, projectId, parentId)
        index("prompt_dependencies_project_id_child_name", false, projectId, childName)
    }
}
