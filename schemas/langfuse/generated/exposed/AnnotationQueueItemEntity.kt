package langfuse.models

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

    var queueId by AnnotationQueueItemTable.queueId
    var queue by AnnotationQueueEntity referencedOn AnnotationQueueItemTable.queueId
    var objectId by AnnotationQueueItemTable.objectId
    var objectType by AnnotationQueueItemTable.objectType
    var status by AnnotationQueueItemTable.status
    var lockedAt by AnnotationQueueItemTable.lockedAt
    var lockedByUserId by AnnotationQueueItemTable.lockedByUserId
    var lockedByUser by UserEntity optionalReferencedOn AnnotationQueueItemTable.lockedByUserId
    var annotatorUserId by AnnotationQueueItemTable.annotatorUserId
    var annotatorUser by UserEntity optionalReferencedOn AnnotationQueueItemTable.annotatorUserId
    var completedAt by AnnotationQueueItemTable.completedAt
    var projectId by AnnotationQueueItemTable.projectId
    var project by ProjectEntity referencedOn AnnotationQueueItemTable.projectId
    var createdAt by AnnotationQueueItemTable.createdAt
    var updatedAt by AnnotationQueueItemTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AnnotationQueueItemTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
