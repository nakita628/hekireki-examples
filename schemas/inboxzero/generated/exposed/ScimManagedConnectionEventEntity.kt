package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimManagedConnectionEventEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimManagedConnectionEventEntity>(ScimManagedConnectionEventTable)

    var connectionRecordId by ScimManagedConnectionEventTable.connectionRecordId
    var scimmanagedconnection by ScimManagedConnectionEntity referencedOn ScimManagedConnectionEventTable.connectionRecordId
    var eventKey by ScimManagedConnectionEventTable.eventKey
    var sequence by ScimManagedConnectionEventTable.sequence
    var type by ScimManagedConnectionEventTable.type
    var actorId by ScimManagedConnectionEventTable.actorId
    var credentialId by ScimManagedConnectionEventTable.credentialId
    var createdAt by ScimManagedConnectionEventTable.createdAt
}
