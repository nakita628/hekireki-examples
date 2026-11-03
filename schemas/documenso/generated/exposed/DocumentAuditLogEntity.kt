package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DocumentAuditLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentAuditLogEntity>(DocumentAuditLogTable)

    var envelopeId by DocumentAuditLogTable.envelopeId
    var createdAt by DocumentAuditLogTable.createdAt
    var type by DocumentAuditLogTable.type
    var data by DocumentAuditLogTable.data
    var name by DocumentAuditLogTable.name
    var email by DocumentAuditLogTable.email
    var userId by DocumentAuditLogTable.userId
    var userAgent by DocumentAuditLogTable.userAgent
    var ipAddress by DocumentAuditLogTable.ipAddress
    var envelope by EnvelopeEntity optionalReferencedOn DocumentAuditLogTable.envelopeId
}
