package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ImpersonationAuditLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ImpersonationAuditLogEntity>(ImpersonationAuditLogTable)

    var action by ImpersonationAuditLogTable.action
    var admin by UserEntity referencedOn ImpersonationAuditLogTable.adminId
    var adminId by ImpersonationAuditLogTable.adminId
    var target by UserEntity referencedOn ImpersonationAuditLogTable.targetId
    var targetId by ImpersonationAuditLogTable.targetId
    var ipAddress by ImpersonationAuditLogTable.ipAddress
    var createdAt by ImpersonationAuditLogTable.createdAt
}
