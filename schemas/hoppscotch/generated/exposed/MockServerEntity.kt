package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MockServerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MockServerEntity>(MockServerTable)

    var name by MockServerTable.name
    var subdomain by MockServerTable.subdomain
    var creatorUid by MockServerTable.creatorUid
    var collectionID by MockServerTable.collectionID
    var workspaceType by MockServerTable.workspaceType
    var workspaceID by MockServerTable.workspaceID
    var delayInMs by MockServerTable.delayInMs
    var isPublic by MockServerTable.isPublic
    var isActive by MockServerTable.isActive
    var hitCount by MockServerTable.hitCount
    var lastHitAt by MockServerTable.lastHitAt
    var createdOn by MockServerTable.createdOn
    var updatedOn by MockServerTable.updatedOn
    var deletedAt by MockServerTable.deletedAt
    var user by UserEntity optionalReferencedOn MockServerTable.creatorUid
    val requestLogs by MockServerLogEntity referrersOn MockServerLogTable.mockServerID
    val activityHistory by MockServerActivityEntity referrersOn MockServerActivityTable.mockServerID

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MockServerTable.updatedOn)) {
                updatedOn = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
