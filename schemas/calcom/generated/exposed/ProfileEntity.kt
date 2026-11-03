package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProfileEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, ProfileEntity>(ProfileTable)

    var uid by ProfileTable.uid
    var userId by ProfileTable.userId
    var user by UserEntity referencedOn ProfileTable.userId
    var organizationId by ProfileTable.organizationId
    var organization by TeamEntity referencedOn ProfileTable.organizationId
    var username by ProfileTable.username
    val eventTypes by EventTypeEntity optionalReferrersOn EventTypeTable.profileId
    val movedFromUser by UserEntity optionalBackReferencedOn UserTable.movedToProfileId
    var createdAt by ProfileTable.createdAt
    var updatedAt by ProfileTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProfileTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
