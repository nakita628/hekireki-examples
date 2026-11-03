package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TriggerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TriggerEntity>(TriggerTable)

    var createdAt by TriggerTable.createdAt
    var updatedAt by TriggerTable.updatedAt
    var projectId by TriggerTable.projectId
    var project by ProjectEntity referencedOn TriggerTable.projectId
    var eventSource by TriggerTable.eventSource
    var eventActions by TriggerTable.eventActions
    var filter by TriggerTable.filter
    var status by TriggerTable.status
    val automationExecutions by AutomationExecutionEntity referrersOn AutomationExecutionTable.triggerId
    val automations by AutomationEntity referrersOn AutomationTable.triggerId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TriggerTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
