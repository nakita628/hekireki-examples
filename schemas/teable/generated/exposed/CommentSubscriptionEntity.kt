package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CommentSubscriptionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CommentSubscriptionEntity>(CommentSubscriptionTable)

    var tableId by CommentSubscriptionTable.tableId
    var recordId by CommentSubscriptionTable.recordId
    var createdBy by CommentSubscriptionTable.createdBy
    var createdTime by CommentSubscriptionTable.createdTime
}
