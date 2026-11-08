package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FeedbacksEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FeedbacksEntity>(FeedbacksTable)

    var review by FeedbacksTable.review
    var message by FeedbacksTable.message
    var email by FeedbacksTable.email
    var userId by FeedbacksTable.userId
    var user by UserEntity optionalReferencedOn FeedbacksTable.userId
    var createdAt by FeedbacksTable.createdAt
    var updatedAt by FeedbacksTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FeedbacksTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
