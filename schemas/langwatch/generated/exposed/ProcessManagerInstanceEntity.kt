package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProcessManagerInstanceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProcessManagerInstanceEntity>(ProcessManagerInstanceTable)

    var processName by ProcessManagerInstanceTable.processName
    var projectId by ProcessManagerInstanceTable.projectId
    var processKey by ProcessManagerInstanceTable.processKey
    var tenantId by ProcessManagerInstanceTable.tenantId
    var userId by ProcessManagerInstanceTable.userId
    var state by ProcessManagerInstanceTable.state
    var revision by ProcessManagerInstanceTable.revision
    var nextWakeAt by ProcessManagerInstanceTable.nextWakeAt
    var updatedAt by ProcessManagerInstanceTable.updatedAt
}
