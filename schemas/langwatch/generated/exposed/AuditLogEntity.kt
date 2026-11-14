package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AuditLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AuditLogEntity>(AuditLogTable)

    var createdAt by AuditLogTable.createdAt
    var userId by AuditLogTable.userId
    var projectId by AuditLogTable.projectId
    var organizationId by AuditLogTable.organizationId
    var action by AuditLogTable.action
    var args by AuditLogTable.args
    var error by AuditLogTable.error
    var ipAddress by AuditLogTable.ipAddress
    var userAgent by AuditLogTable.userAgent
    var metadata by AuditLogTable.metadata
    var actorUserId by AuditLogTable.actorUserId
    var targetKind by AuditLogTable.targetKind
    var targetId by AuditLogTable.targetId
    var before by AuditLogTable.before
    var after by AuditLogTable.after
    var idempotencyKey by AuditLogTable.idempotencyKey
}
