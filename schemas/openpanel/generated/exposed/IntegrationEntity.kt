package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class IntegrationEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, IntegrationEntity>(IntegrationTable)

    var name by IntegrationTable.name
    var config by IntegrationTable.config
    var organization by OrganizationEntity referencedOn IntegrationTable.organizationId
    var organizationId by IntegrationTable.organizationId
    var project by ProjectEntity optionalReferencedOn IntegrationTable.projectId
    var projectId by IntegrationTable.projectId
    var notificationRules by NotificationRuleEntity via IntegrationToNotificationRuleTable
    val notifications by NotificationEntity optionalReferrersOn NotificationTable.integrationId
    val exportWatermarks by ExportWatermarkEntity referrersOn ExportWatermarkTable.integrationId
    var createdAt by IntegrationTable.createdAt
    var updatedAt by IntegrationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IntegrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
