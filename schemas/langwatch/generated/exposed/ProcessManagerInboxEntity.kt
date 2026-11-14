package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProcessManagerInboxEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProcessManagerInboxEntity>(ProcessManagerInboxTable)

    var processName by ProcessManagerInboxTable.processName
    var projectId by ProcessManagerInboxTable.projectId
    var processKey by ProcessManagerInboxTable.processKey
    var tenantId by ProcessManagerInboxTable.tenantId
    var sourceEventId by ProcessManagerInboxTable.sourceEventId
    var sourceEventKey by ProcessManagerInboxTable.sourceEventKey
    var consumedAt by ProcessManagerInboxTable.consumedAt
}
