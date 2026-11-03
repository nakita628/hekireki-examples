package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AttributeToUserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AttributeToUserEntity>(AttributeToUserTable)

    var member by MembershipEntity referencedOn AttributeToUserTable.memberId
    var memberId by AttributeToUserTable.memberId
    var attributeOption by AttributeOptionEntity referencedOn AttributeToUserTable.attributeOptionId
    var attributeOptionId by AttributeToUserTable.attributeOptionId
    var weight by AttributeToUserTable.weight
    var createdAt by AttributeToUserTable.createdAt
    var createdById by AttributeToUserTable.createdById
    var createdBy by UserEntity optionalReferencedOn AttributeToUserTable.createdById
    var createdByDSyncId by AttributeToUserTable.createdByDSyncId
    var createdByDSync by DSyncDataEntity optionalReferencedOn AttributeToUserTable.createdByDSyncId
    var updatedAt by AttributeToUserTable.updatedAt
    var updatedBy by UserEntity optionalReferencedOn AttributeToUserTable.updatedById
    var updatedById by AttributeToUserTable.updatedById
    var updatedByDSyncId by AttributeToUserTable.updatedByDSyncId
    var updatedByDSync by DSyncDataEntity optionalReferencedOn AttributeToUserTable.updatedByDSyncId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AttributeToUserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
