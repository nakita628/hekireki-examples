package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PlanProviderMappingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PlanProviderMappingEntity>(PlanProviderMappingTable)

    var planId by PlanProviderMappingTable.planId
    var provider by PlanProviderMappingTable.provider
    var externalId by PlanProviderMappingTable.externalId
    var region by PlanProviderMappingTable.region
    var metadata by PlanProviderMappingTable.metadata
    var isActive by PlanProviderMappingTable.isActive
    var createdAt by PlanProviderMappingTable.createdAt
    var updatedAt by PlanProviderMappingTable.updatedAt
    var plan by SubscriptionPlanEntity referencedOn PlanProviderMappingTable.planId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PlanProviderMappingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
