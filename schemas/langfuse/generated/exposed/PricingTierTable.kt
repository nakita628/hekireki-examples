package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PricingTierTable : IdTable<String>("pricing_tiers") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val modelId =
        reference(
            "model_id",
            ModelTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "pricing_tiers_model_id_fkey",
        )
    val name = text("name")
    val isDefault = bool("is_default").default(false)
    val priority = integer("priority")
    val conditions = jsonb<String>("conditions", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "pricing_tiers_pkey")

    init {
        uniqueIndex("pricing_tiers_model_id_priority_key", modelId, priority)
        uniqueIndex("pricing_tiers_model_id_name_key", modelId, name)
    }
}
