package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AutomationExecutionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AutomationExecutionEntity>(AutomationExecutionTable)

    var createdAt by AutomationExecutionTable.createdAt
    var updatedAt by AutomationExecutionTable.updatedAt
    var sourceId by AutomationExecutionTable.sourceId
    var automationId by AutomationExecutionTable.automationId
    var automation by AutomationEntity referencedOn AutomationExecutionTable.automationId
    var triggerId by AutomationExecutionTable.triggerId
    var trigger by TriggerEntity referencedOn AutomationExecutionTable.triggerId
    var actionId by AutomationExecutionTable.actionId
    var action by ActionEntity referencedOn AutomationExecutionTable.actionId
    var projectId by AutomationExecutionTable.projectId
    var project by ProjectEntity referencedOn AutomationExecutionTable.projectId
    var status by AutomationExecutionTable.status
    var input by AutomationExecutionTable.input
    var output by AutomationExecutionTable.output
    var startedAt by AutomationExecutionTable.startedAt
    var finishedAt by AutomationExecutionTable.finishedAt
    var error by AutomationExecutionTable.error

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AutomationExecutionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
