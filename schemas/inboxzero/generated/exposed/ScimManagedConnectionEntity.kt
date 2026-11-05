package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimManagedConnectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimManagedConnectionEntity>(ScimManagedConnectionTable)

    var creationRequestId by ScimManagedConnectionTable.creationRequestId
    var connectionId by ScimManagedConnectionTable.connectionId
    var provisioningDomainId by ScimManagedConnectionTable.provisioningDomainId
    var status by ScimManagedConnectionTable.status
    var revision by ScimManagedConnectionTable.revision
    var createdAt by ScimManagedConnectionTable.createdAt
    var createdBy by ScimManagedConnectionTable.createdBy
    var decommissionStartedAt by ScimManagedConnectionTable.decommissionStartedAt
    var decommissionStartedBy by ScimManagedConnectionTable.decommissionStartedBy
    var decommissionedAt by ScimManagedConnectionTable.decommissionedAt
    var decommissionedBy by ScimManagedConnectionTable.decommissionedBy
    val scimmanagedcredentials by ScimManagedCredentialEntity referrersOn ScimManagedCredentialTable.connectionRecordId
    val scimmanagedconnectionevents by ScimManagedConnectionEventEntity referrersOn ScimManagedConnectionEventTable.connectionRecordId
}
