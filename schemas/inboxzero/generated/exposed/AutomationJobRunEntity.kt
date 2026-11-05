package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AutomationJobRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AutomationJobRunEntity>(AutomationJobRunTable)

    var createdAt by AutomationJobRunTable.createdAt
    var status by AutomationJobRunTable.status
    var scheduledFor by AutomationJobRunTable.scheduledFor
    var processedAt by AutomationJobRunTable.processedAt
    var outboundMessage by AutomationJobRunTable.outboundMessage
    var providerMessageId by AutomationJobRunTable.providerMessageId
    var error by AutomationJobRunTable.error
    var automationJobId by AutomationJobRunTable.automationJobId
    var automationJob by AutomationJobEntity referencedOn AutomationJobRunTable.automationJobId
}
