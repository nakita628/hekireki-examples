package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AuditLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AuditLogEntity>(AuditLogTable)

    var timestamp by AuditLogTable.timestamp
    var type by AuditLogTable.type
    var severity by AuditLogTable.severity
    var userId by AuditLogTable.userId
    var workspaceId by AuditLogTable.workspaceId
    var objectId by AuditLogTable.objectId
    var changes by AuditLogTable.changes
    var authType by AuditLogTable.authType
    var tokenId by AuditLogTable.tokenId
    var ip by AuditLogTable.ip
    var requestHeaders by AuditLogTable.requestHeaders
}
