package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object LlmPricingTierTable : IdTable<String>("llm_pricing_tiers") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val modelId =
        reference(
            "model_id",
            LlmModelTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "llm_pricing_tiers_model_id_fkey",
        )
    val name = text("name")
    val isDefault = bool("is_default").default(true)
    val priority = integer("priority").default(0)
    val conditions = jsonb<String>("conditions", { it }, { it }).default("[]")
    override val primaryKey = PrimaryKey(id, name = "llm_pricing_tiers_pkey")

    init {
        uniqueIndex("llm_pricing_tiers_model_id_priority_key", modelId, priority)
        uniqueIndex("llm_pricing_tiers_model_id_name_key", modelId, name)
    }
}
