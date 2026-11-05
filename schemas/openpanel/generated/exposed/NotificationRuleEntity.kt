package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class NotificationRuleEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, NotificationRuleEntity>(NotificationRuleTable)

    var name by NotificationRuleTable.name
    var projectId by NotificationRuleTable.projectId
    var project by ProjectEntity referencedOn NotificationRuleTable.projectId
    var integrations by IntegrationEntity via IntegrationToNotificationRuleTable
    var sendToApp by NotificationRuleTable.sendToApp
    var sendToEmail by NotificationRuleTable.sendToEmail
    var config by NotificationRuleTable.config
    var template by NotificationRuleTable.template
    var createdAt by NotificationRuleTable.createdAt
    var updatedAt by NotificationRuleTable.updatedAt
    val notifications by NotificationEntity optionalReferrersOn NotificationTable.notificationRuleId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(NotificationRuleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
