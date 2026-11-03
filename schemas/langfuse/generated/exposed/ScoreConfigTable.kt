package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScoreConfigTable : IdTable<String>("score_configs") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "score_configs_project_id_fkey",
        )
    val name = text("name")
    val dataType = pgEnum("data_type", "\"ScoreConfigDataType\"", ScoreConfigDataType.entries, ScoreConfigDataType::dbName)
    val isArchived = bool("is_archived").default(false)
    val minValue = double("min_value").nullable()
    val maxValue = double("max_value").nullable()
    val categories = jsonb<String>("categories", { it }, { it }).nullable()
    val description = text("description").nullable()
    override val primaryKey = PrimaryKey(id, name = "score_configs_pkey")

    init {
        index("score_configs_data_type_idx", false, dataType)
        index("score_configs_is_archived_idx", false, isArchived)
        index("score_configs_project_id_idx", false, projectId)
        index("score_configs_categories_idx", false, categories)
        index("score_configs_created_at_idx", false, createdAt)
        index("score_configs_updated_at_idx", false, updatedAt)
        uniqueIndex("score_configs_id_project_id_key", id, projectId)
    }
}
