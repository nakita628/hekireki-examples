package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class InvitationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InvitationEntity>(InvitationTable)

    var baseId by InvitationTable.baseId
    var spaceId by InvitationTable.spaceId
    var type by InvitationTable.type
    var role by InvitationTable.role
    var invitationCode by InvitationTable.invitationCode
    var expiredTime by InvitationTable.expiredTime
    var createdBy by InvitationTable.createdBy
    var createdTime by InvitationTable.createdTime
    var lastModifiedTime by InvitationTable.lastModifiedTime
    var lastModifiedBy by InvitationTable.lastModifiedBy
    var deletedTime by InvitationTable.deletedTime

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InvitationTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
