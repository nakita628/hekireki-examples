package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PriceTable : IdTable<String>("prices") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val modelId =
        reference(
            "model_id",
            ModelTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "prices_model_id_fkey",
        )
    val projectId =
        optReference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "prices_project_id_fkey",
        )
    val pricingTierId =
        reference(
            "pricing_tier_id",
            PricingTierTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "prices_pricing_tier_id_fkey",
        )
    val usageType = text("usage_type")
    val price = decimal("price", 65, 30)
    override val primaryKey = PrimaryKey(id, name = "prices_pkey")

    init {
        index("prices_pricing_tier_id_idx", false, pricingTierId)
        uniqueIndex("prices_model_id_usage_type_pricing_tier_id_key", modelId, usageType, pricingTierId)
    }
}
