package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LlmPriceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LlmPriceEntity>(LlmPriceTable)

    var modelId by LlmPriceTable.modelId
    var model by LlmModelEntity referencedOn LlmPriceTable.modelId
    var pricingTierId by LlmPriceTable.pricingTierId
    var pricingTier by LlmPricingTierEntity referencedOn LlmPriceTable.pricingTierId
    var usageType by LlmPriceTable.usageType
    var price by LlmPriceTable.price
}
