package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ActionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ActionEntity>(ActionTable)

    var createdAt by ActionTable.createdAt
    var updatedAt by ActionTable.updatedAt
    var projectId by ActionTable.projectId
    var project by ProjectEntity referencedOn ActionTable.projectId
    var type by ActionTable.type
    var config by ActionTable.config
    val automations by AutomationEntity referrersOn AutomationTable.actionId
    val automationExecutions by AutomationExecutionEntity referrersOn AutomationExecutionTable.actionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ActionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
