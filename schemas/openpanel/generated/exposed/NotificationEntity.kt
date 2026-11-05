package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class NotificationEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, NotificationEntity>(NotificationTable)

    var projectId by NotificationTable.projectId
    var project by ProjectEntity referencedOn NotificationTable.projectId
    var title by NotificationTable.title
    var message by NotificationTable.message
    var isReadAt by NotificationTable.isReadAt
    var createdAt by NotificationTable.createdAt
    var updatedAt by NotificationTable.updatedAt
    var sendToApp by NotificationTable.sendToApp
    var sendToEmail by NotificationTable.sendToEmail
    var integration by IntegrationEntity optionalReferencedOn NotificationTable.integrationId
    var integrationId by NotificationTable.integrationId
    var notificationRuleId by NotificationTable.notificationRuleId
    var notificationRule by NotificationRuleEntity optionalReferencedOn NotificationTable.notificationRuleId
    var payload by NotificationTable.payload

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(NotificationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
