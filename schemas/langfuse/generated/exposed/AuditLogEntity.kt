package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AuditLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AuditLogEntity>(AuditLogTable)

    var createdAt by AuditLogTable.createdAt
    var updatedAt by AuditLogTable.updatedAt
    var type by AuditLogTable.type
    var apiKeyId by AuditLogTable.apiKeyId
    var userId by AuditLogTable.userId
    var orgId by AuditLogTable.orgId
    var userOrgRole by AuditLogTable.userOrgRole
    var projectId by AuditLogTable.projectId
    var userProjectRole by AuditLogTable.userProjectRole
    var resourceType by AuditLogTable.resourceType
    var resourceId by AuditLogTable.resourceId
    var action by AuditLogTable.action
    var before by AuditLogTable.before
    var after by AuditLogTable.after

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AuditLogTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
