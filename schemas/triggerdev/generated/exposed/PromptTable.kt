package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PromptTable : IdTable<String>("prompts") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendly_id").uniqueIndex("prompts_friendly_id_key")
    val slug = text("slug")
    val description = text("description").nullable()
    val type = text("type").default("text")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"prompts_organizationId_fkey\"",
        )
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"prompts_projectId_fkey\"",
        )
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"prompts_runtimeEnvironmentId_fkey\"",
        )
    val filePath = text("filePath").nullable()
    val exportName = text("exportName").nullable()
    val variableSchema = jsonb<String>("variableSchema", { it }, { it }).nullable()
    val defaultModel = text("defaultModel").nullable()
    val defaultConfig = jsonb<String>("defaultConfig", { it }, { it }).nullable()
    val tags = array("tags", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "prompts_pkey")

    init {
        index("\"prompts_projectId_idx\"", false, projectId)
        index("\"prompts_runtimeEnvironmentId_idx\"", false, runtimeEnvironmentId)
        uniqueIndex("\"prompts_projectId_runtimeEnvironmentId_slug_key\"", projectId, runtimeEnvironmentId, slug)
    }
}
