package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LlmModelEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LlmModelEntity>(LlmModelTable)

    var friendlyId by LlmModelTable.friendlyId
    var projectId by LlmModelTable.projectId
    var project by ProjectEntity optionalReferencedOn LlmModelTable.projectId
    var modelName by LlmModelTable.modelName
    var matchPattern by LlmModelTable.matchPattern
    var startDate by LlmModelTable.startDate
    var source by LlmModelTable.source1
    var createdAt by LlmModelTable.createdAt
    var updatedAt by LlmModelTable.updatedAt
    var provider by LlmModelTable.provider
    var description by LlmModelTable.description
    var contextWindow by LlmModelTable.contextWindow
    var maxOutputTokens by LlmModelTable.maxOutputTokens
    var capabilities by LlmModelTable.capabilities
    var isHidden by LlmModelTable.isHidden
    var baseModelName by LlmModelTable.baseModelName
    var pricingUnit by LlmModelTable.pricingUnit
    val pricingTiers by LlmPricingTierEntity referrersOn LlmPricingTierTable.modelId
    val prices by LlmPriceEntity referrersOn LlmPriceTable.modelId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LlmModelTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
