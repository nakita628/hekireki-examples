package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class FeedbackEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, FeedbackEntity>(FeedbackTable)

    var date by FeedbackTable.date
    var userId by FeedbackTable.userId
    var user by UserEntity referencedOn FeedbackTable.userId
    var rating by FeedbackTable.rating
    var comment by FeedbackTable.comment
}
