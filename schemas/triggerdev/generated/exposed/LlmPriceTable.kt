package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object LlmPriceTable : IdTable<String>("llm_prices") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val modelId =
        reference(
            "model_id",
            LlmModelTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "llm_prices_model_id_fkey",
        )
    val pricingTierId =
        reference(
            "pricing_tier_id",
            LlmPricingTierTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "llm_prices_pricing_tier_id_fkey",
        )
    val usageType = text("usage_type")
    val price = decimal("price", 20, 12)
    override val primaryKey = PrimaryKey(id, name = "llm_prices_pkey")

    init {
        uniqueIndex("llm_prices_model_id_usage_type_pricing_tier_id_key", modelId, usageType, pricingTierId)
    }
}
