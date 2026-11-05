package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimConnectionBindingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimConnectionBindingEntity>(ScimConnectionBindingTable)

    var connectionId by ScimConnectionBindingTable.connectionId
    var connectionKey by ScimConnectionBindingTable.connectionKey
    var provisioningDomainId by ScimConnectionBindingTable.provisioningDomainId
    var createdAt by ScimConnectionBindingTable.createdAt
    var decommissionedAt by ScimConnectionBindingTable.decommissionedAt
    var decommissionStatus by ScimConnectionBindingTable.decommissionStatus
    var decommissionCursorUserId by ScimConnectionBindingTable.decommissionCursorUserId
    var decommissionReconciledUserCount by ScimConnectionBindingTable.decommissionReconciledUserCount
    var decommissionBatchCount by ScimConnectionBindingTable.decommissionBatchCount
    var decommissionRevision by ScimConnectionBindingTable.decommissionRevision
    var decommissionCompletedAt by ScimConnectionBindingTable.decommissionCompletedAt
    var decommissionLeaseId by ScimConnectionBindingTable.decommissionLeaseId
    var decommissionLeaseExpiresAt by ScimConnectionBindingTable.decommissionLeaseExpiresAt
}
