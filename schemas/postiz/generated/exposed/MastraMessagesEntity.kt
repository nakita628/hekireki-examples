package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraMessagesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraMessagesEntity>(MastraMessagesTable)

    var threadId by MastraMessagesTable.threadId
    var content by MastraMessagesTable.content
    var role by MastraMessagesTable.role
    var type by MastraMessagesTable.type
    var createdAt by MastraMessagesTable.createdAt
    var resourceId by MastraMessagesTable.resourceId
    var createdAtZ by MastraMessagesTable.createdAtZ
}
