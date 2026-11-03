package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class AuditLogStagingEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, AuditLogStagingEntity>(AuditLogStagingTable)

    var createdAt by AuditLogStagingTable.createdAt
    var payload by AuditLogStagingTable.payload
    var failedAt by AuditLogStagingTable.failedAt
    var lastError by AuditLogStagingTable.lastError
}
