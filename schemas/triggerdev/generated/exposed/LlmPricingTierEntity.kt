package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LlmPricingTierEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LlmPricingTierEntity>(LlmPricingTierTable)

    var modelId by LlmPricingTierTable.modelId
    var model by LlmModelEntity referencedOn LlmPricingTierTable.modelId
    var name by LlmPricingTierTable.name
    var isDefault by LlmPricingTierTable.isDefault
    var priority by LlmPricingTierTable.priority
    var conditions by LlmPricingTierTable.conditions
    val prices by LlmPriceEntity referrersOn LlmPriceTable.pricingTierId
}
