package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OutboundEmailAuditErasureIntentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OutboundEmailAuditErasureIntentEntity>(OutboundEmailAuditErasureIntentTable)

    var userId by OutboundEmailAuditErasureIntentTable.userId
    var userHash by OutboundEmailAuditErasureIntentTable.userHash
    var recipientMac by OutboundEmailAuditErasureIntentTable.recipientMac
    var createdAt by OutboundEmailAuditErasureIntentTable.createdAt
    var activatedAt by OutboundEmailAuditErasureIntentTable.activatedAt
    var checkedAt by OutboundEmailAuditErasureIntentTable.checkedAt
    var expiresAt by OutboundEmailAuditErasureIntentTable.expiresAt
}
