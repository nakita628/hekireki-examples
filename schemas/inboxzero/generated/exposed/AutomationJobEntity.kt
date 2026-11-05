package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AutomationJobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AutomationJobEntity>(AutomationJobTable)

    var createdAt by AutomationJobTable.createdAt
    var updatedAt by AutomationJobTable.updatedAt
    var name by AutomationJobTable.name
    var enabled by AutomationJobTable.enabled
    var prompt by AutomationJobTable.prompt
    var cronExpression by AutomationJobTable.cronExpression
    var nextRunAt by AutomationJobTable.nextRunAt
    var messagingChannelId by AutomationJobTable.messagingChannelId
    var emailAccountId by AutomationJobTable.emailAccountId
    val runs by AutomationJobRunEntity referrersOn AutomationJobRunTable.automationJobId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AutomationJobTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
