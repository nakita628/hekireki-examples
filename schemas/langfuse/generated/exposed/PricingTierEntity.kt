package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PricingTierEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PricingTierEntity>(PricingTierTable)

    var createdAt by PricingTierTable.createdAt
    var updatedAt by PricingTierTable.updatedAt
    var modelId by PricingTierTable.modelId
    var model by ModelEntity referencedOn PricingTierTable.modelId
    var name by PricingTierTable.name
    var isDefault by PricingTierTable.isDefault
    var priority by PricingTierTable.priority
    var conditions by PricingTierTable.conditions
    val prices by PriceEntity referrersOn PriceTable.pricingTierId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PricingTierTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
