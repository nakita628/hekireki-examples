package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ModelEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ModelEntity>(ModelTable)

    var createdAt by ModelTable.createdAt
    var updatedAt by ModelTable.updatedAt
    var projectId by ModelTable.projectId
    var project by ProjectEntity optionalReferencedOn ModelTable.projectId
    var modelName by ModelTable.modelName
    var matchPattern by ModelTable.matchPattern
    var startDate by ModelTable.startDate
    var inputPrice by ModelTable.inputPrice
    var outputPrice by ModelTable.outputPrice
    var totalPrice by ModelTable.totalPrice
    var unit by ModelTable.unit
    var tokenizerId by ModelTable.tokenizerId
    var tokenizerConfig by ModelTable.tokenizerConfig
    val price by PriceEntity referrersOn PriceTable.modelId
    val pricingTiers by PricingTierEntity referrersOn PricingTierTable.modelId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ModelTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
