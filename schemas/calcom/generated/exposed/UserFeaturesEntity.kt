package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserFeaturesEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<UserFeaturesEntity>(UserFeaturesTable)

    val user by UserEntity referencedOn UserFeaturesTable.userId
    val userId by UserFeaturesTable.userId
    val feature by FeatureEntity referencedOn UserFeaturesTable.featureId
    val featureId by UserFeaturesTable.featureId
    var enabled by UserFeaturesTable.enabled
    var assignedAt by UserFeaturesTable.assignedAt
    var assignedBy by UserFeaturesTable.assignedBy
    var updatedAt by UserFeaturesTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserFeaturesTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
