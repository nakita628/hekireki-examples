package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PlatformNotificationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PlatformNotificationEntity>(PlatformNotificationTable)

    var friendlyId by PlatformNotificationTable.friendlyId
    var title by PlatformNotificationTable.title
    var payload by PlatformNotificationTable.payload
    var surface by PlatformNotificationTable.surface
    var scope by PlatformNotificationTable.scope
    var user by UserEntity optionalReferencedOn PlatformNotificationTable.userId
    var userId by PlatformNotificationTable.userId
    var organization by OrganizationEntity optionalReferencedOn PlatformNotificationTable.organizationId
    var organizationId by PlatformNotificationTable.organizationId
    var project by ProjectEntity optionalReferencedOn PlatformNotificationTable.projectId
    var projectId by PlatformNotificationTable.projectId
    var startsAt by PlatformNotificationTable.startsAt
    var endsAt by PlatformNotificationTable.endsAt
    var cliMaxDaysAfterFirstSeen by PlatformNotificationTable.cliMaxDaysAfterFirstSeen
    var cliMaxShowCount by PlatformNotificationTable.cliMaxShowCount
    var cliShowEvery by PlatformNotificationTable.cliShowEvery
    var priority by PlatformNotificationTable.priority
    var isDraft by PlatformNotificationTable.isDraft
    var archivedAt by PlatformNotificationTable.archivedAt
    var createdAt by PlatformNotificationTable.createdAt
    var updatedAt by PlatformNotificationTable.updatedAt
    val interactions by PlatformNotificationInteractionEntity referrersOn PlatformNotificationInteractionTable.notificationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PlatformNotificationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
