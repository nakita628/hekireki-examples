package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ContactFormMessageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ContactFormMessageEntity>(ContactFormMessageTable)

    var createdAt by ContactFormMessageTable.createdAt
    var user by UserEntity referencedOn ContactFormMessageTable.userId
    var userId by ContactFormMessageTable.userId
    var content by ContactFormMessageTable.content
    var isRead by ContactFormMessageTable.isRead
    var repliedAt by ContactFormMessageTable.repliedAt
}
