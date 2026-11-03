package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TagEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TagEntity>(TagTable)

    val accounts by TagsOnAccountsEntity referrersOn TagsOnAccountsTable.tagId
    var activities by OrderEntity via OrderToTagTable
    var name by TagTable.name
    var user by UserEntity optionalReferencedOn TagTable.userId
    var userId by TagTable.userId
}
