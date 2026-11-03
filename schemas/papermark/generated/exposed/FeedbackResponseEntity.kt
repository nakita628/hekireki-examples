package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FeedbackResponseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FeedbackResponseEntity>(FeedbackResponseTable)

    var feedbackId by FeedbackResponseTable.feedbackId
    var feedback by FeedbackEntity referencedOn FeedbackResponseTable.feedbackId
    var data by FeedbackResponseTable.data
    var viewId by FeedbackResponseTable.viewId
    var view by ViewEntity referencedOn FeedbackResponseTable.viewId
    var createdAt by FeedbackResponseTable.createdAt
    var updatedAt by FeedbackResponseTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FeedbackResponseTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
