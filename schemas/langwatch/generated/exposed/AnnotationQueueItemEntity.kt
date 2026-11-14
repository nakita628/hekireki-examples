package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AnnotationQueueItemEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnnotationQueueItemEntity>(AnnotationQueueItemTable)

    var annotationQueueId by AnnotationQueueItemTable.annotationQueueId
    var userId by AnnotationQueueItemTable.userId
    var createdAt by AnnotationQueueItemTable.createdAt
    var updatedAt by AnnotationQueueItemTable.updatedAt
    var createdByUserId by AnnotationQueueItemTable.createdByUserId
    var traceId by AnnotationQueueItemTable.traceId
    var projectId by AnnotationQueueItemTable.projectId
    var doneAt by AnnotationQueueItemTable.doneAt
    var markedForDatasetAt by AnnotationQueueItemTable.markedForDatasetAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AnnotationQueueItemTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
