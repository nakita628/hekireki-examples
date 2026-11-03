package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserSecurityAuditLogEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, UserSecurityAuditLogEntity>(UserSecurityAuditLogTable)

    var userId by UserSecurityAuditLogTable.userId
    var createdAt by UserSecurityAuditLogTable.createdAt
    var type by UserSecurityAuditLogTable.type
    var userAgent by UserSecurityAuditLogTable.userAgent
    var ipAddress by UserSecurityAuditLogTable.ipAddress
    var user by UserEntity referencedOn UserSecurityAuditLogTable.userId
}
