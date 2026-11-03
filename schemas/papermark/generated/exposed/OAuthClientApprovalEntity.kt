package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OAuthClientApprovalEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthClientApprovalEntity>(OAuthClientApprovalTable)

    var userId by OAuthClientApprovalTable.userId
    var clientId by OAuthClientApprovalTable.clientId
    var approvedAt by OAuthClientApprovalTable.approvedAt
    var revokedAt by OAuthClientApprovalTable.revokedAt
    var clientMetadataHash by OAuthClientApprovalTable.clientMetadataHash
}
