package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ReactionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ReactionEntity>(ReactionTable)

    var view by ViewEntity referencedOn ReactionTable.viewId
    var viewId by ReactionTable.viewId
    var pageNumber by ReactionTable.pageNumber
    var type by ReactionTable.type
    var createdAt by ReactionTable.createdAt
}
