package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LlmModelTable : IdTable<String>("llm_models") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendly_id").uniqueIndex("llm_models_friendly_id_key")
    val projectId =
        optReference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "llm_models_project_id_fkey",
        )
    val modelName = text("model_name")
    val matchPattern = text("match_pattern")
    val startDate = pgTimestamp("start_date", 3).nullable()
    val source1 = text("source").default("default")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val provider = text("provider").nullable()
    val description = text("description").nullable()
    val contextWindow = integer("context_window").nullable()
    val maxOutputTokens = integer("max_output_tokens").nullable()
    val capabilities = array("capabilities", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val isHidden = bool("is_hidden").default(false)
    val baseModelName = text("base_model_name").nullable()
    val pricingUnit = text("pricing_unit").nullable()
    override val primaryKey = PrimaryKey(id, name = "llm_models_pkey")

    init {
        index("llm_models_project_id_idx", false, projectId)
        uniqueIndex("llm_models_project_id_model_name_start_date_key", projectId, modelName, startDate)
    }
}
