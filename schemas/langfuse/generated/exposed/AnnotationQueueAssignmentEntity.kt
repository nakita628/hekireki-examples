package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AnnotationQueueAssignmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnnotationQueueAssignmentEntity>(AnnotationQueueAssignmentTable)

    var projectId by AnnotationQueueAssignmentTable.projectId
    var project by ProjectEntity referencedOn AnnotationQueueAssignmentTable.projectId
    var userId by AnnotationQueueAssignmentTable.userId
    var user by UserEntity referencedOn AnnotationQueueAssignmentTable.userId
    var queueId by AnnotationQueueAssignmentTable.queueId
    var queue by AnnotationQueueEntity referencedOn AnnotationQueueAssignmentTable.queueId
    var createdAt by AnnotationQueueAssignmentTable.createdAt
    var updatedAt by AnnotationQueueAssignmentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AnnotationQueueAssignmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
