package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PlatformNotificationInteractionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PlatformNotificationInteractionEntity>(PlatformNotificationInteractionTable)

    var notification by PlatformNotificationEntity referencedOn PlatformNotificationInteractionTable.notificationId
    var notificationId by PlatformNotificationInteractionTable.notificationId
    var user by UserEntity referencedOn PlatformNotificationInteractionTable.userId
    var userId by PlatformNotificationInteractionTable.userId
    var firstSeenAt by PlatformNotificationInteractionTable.firstSeenAt
    var showCount by PlatformNotificationInteractionTable.showCount
    var webappDismissedAt by PlatformNotificationInteractionTable.webappDismissedAt
    var webappClickedAt by PlatformNotificationInteractionTable.webappClickedAt
    var cliDismissedAt by PlatformNotificationInteractionTable.cliDismissedAt
    var createdAt by PlatformNotificationInteractionTable.createdAt
    var updatedAt by PlatformNotificationInteractionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PlatformNotificationInteractionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
