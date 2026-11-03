package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FeatureEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FeatureEntity>(FeatureTable)

    var enabled by FeatureTable.enabled
    var description by FeatureTable.description
    var type by FeatureTable.type
    var stale by FeatureTable.stale
    var lastUsedAt by FeatureTable.lastUsedAt
    var createdAt by FeatureTable.createdAt
    var updatedAt by FeatureTable.updatedAt
    var updatedBy by FeatureTable.updatedBy
    val users by UserFeaturesEntity referrersOn UserFeaturesTable.featureId
    val teams by TeamFeaturesEntity referrersOn TeamFeaturesTable.featureId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FeatureTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
