package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ModelTable : IdTable<String>("models") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        optReference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "models_project_id_fkey",
        )
    val modelName = text("model_name")
    val matchPattern = text("match_pattern")
    val startDate = pgTimestamp("start_date", 3).nullable()
    val inputPrice = decimal("input_price", 65, 30).nullable()
    val outputPrice = decimal("output_price", 65, 30).nullable()
    val totalPrice = decimal("total_price", 65, 30).nullable()
    val unit = text("unit").nullable()
    val tokenizerId = text("tokenizer_id").nullable()
    val tokenizerConfig = jsonb<String>("tokenizer_config", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "models_pkey")

    init {
        index("models_model_name_idx", false, modelName)
        uniqueIndex("models_project_id_model_name_start_date_unit_key", projectId, modelName, startDate, unit)
    }
}
