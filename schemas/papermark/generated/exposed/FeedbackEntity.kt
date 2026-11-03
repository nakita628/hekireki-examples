package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FeedbackEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FeedbackEntity>(FeedbackTable)

    var linkId by FeedbackTable.linkId
    var link by LinkEntity referencedOn FeedbackTable.linkId
    var data by FeedbackTable.data
    val responses by FeedbackResponseEntity referrersOn FeedbackResponseTable.feedbackId
    var createdAt by FeedbackTable.createdAt
    var updatedAt by FeedbackTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FeedbackTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
