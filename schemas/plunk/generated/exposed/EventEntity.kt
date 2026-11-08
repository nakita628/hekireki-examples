package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class EventEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EventEntity>(EventTable)

    var name by EventTable.name
    var data by EventTable.data
    var project by ProjectEntity referencedOn EventTable.projectId
    var projectId by EventTable.projectId
    var contact by ContactEntity optionalReferencedOn EventTable.contactId
    var contactId by EventTable.contactId
    var email by EmailEntity optionalReferencedOn EventTable.emailId
    var emailId by EventTable.emailId
    var createdAt by EventTable.createdAt
}
