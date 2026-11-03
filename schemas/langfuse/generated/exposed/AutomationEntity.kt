package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AutomationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AutomationEntity>(AutomationTable)

    var name by AutomationTable.name
    var trigger by TriggerEntity referencedOn AutomationTable.triggerId
    var triggerId by AutomationTable.triggerId
    var action by ActionEntity referencedOn AutomationTable.actionId
    var actionId by AutomationTable.actionId
    var createdAt by AutomationTable.createdAt
    var projectId by AutomationTable.projectId
    var project by ProjectEntity referencedOn AutomationTable.projectId
    val automationExecution by AutomationExecutionEntity referrersOn AutomationExecutionTable.automationId
}
