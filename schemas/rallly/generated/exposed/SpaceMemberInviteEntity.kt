package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SpaceMemberInviteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SpaceMemberInviteEntity>(SpaceMemberInviteTable)

    var spaceId by SpaceMemberInviteTable.spaceId
    var email by SpaceMemberInviteTable.email
    var createdAt by SpaceMemberInviteTable.createdAt
    var updatedAt by SpaceMemberInviteTable.updatedAt
    var role by SpaceMemberInviteTable.role
    var inviterId by SpaceMemberInviteTable.inviterId
    var invitedBy by UserEntity referencedOn SpaceMemberInviteTable.inviterId
    var space by SpaceEntity referencedOn SpaceMemberInviteTable.spaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SpaceMemberInviteTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
