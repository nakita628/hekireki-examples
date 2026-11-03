package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SpaceMemberEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SpaceMemberEntity>(SpaceMemberTable)

    var spaceId by SpaceMemberTable.spaceId
    var userId by SpaceMemberTable.userId
    var createdAt by SpaceMemberTable.createdAt
    var updatedAt by SpaceMemberTable.updatedAt
    var role by SpaceMemberTable.role
    var lastSelectedAt by SpaceMemberTable.lastSelectedAt
    var space by SpaceEntity referencedOn SpaceMemberTable.spaceId
    var user by UserEntity referencedOn SpaceMemberTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SpaceMemberTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
