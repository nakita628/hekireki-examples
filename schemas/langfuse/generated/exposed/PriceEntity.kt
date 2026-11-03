package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PriceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PriceEntity>(PriceTable)

    var createdAt by PriceTable.createdAt
    var updatedAt by PriceTable.updatedAt
    var modelId by PriceTable.modelId
    var model by ModelEntity referencedOn PriceTable.modelId
    var projectId by PriceTable.projectId
    var project by ProjectEntity optionalReferencedOn PriceTable.projectId
    var pricingTierId by PriceTable.pricingTierId
    var pricingTier by PricingTierEntity referencedOn PriceTable.pricingTierId
    var usageType by PriceTable.usageType
    var price by PriceTable.price

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PriceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
