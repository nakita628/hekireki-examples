package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AnnotationQueueEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnnotationQueueEntity>(AnnotationQueueTable)

    var name by AnnotationQueueTable.name
    var description by AnnotationQueueTable.description
    var scoreConfigIds by AnnotationQueueTable.scoreConfigIds
    var projectId by AnnotationQueueTable.projectId
    var project by ProjectEntity referencedOn AnnotationQueueTable.projectId
    var createdAt by AnnotationQueueTable.createdAt
    var updatedAt by AnnotationQueueTable.updatedAt
    val annotationQueueItem by AnnotationQueueItemEntity referrersOn AnnotationQueueItemTable.queueId
    val annotationQueueAssignment by AnnotationQueueAssignmentEntity referrersOn AnnotationQueueAssignmentTable.queueId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AnnotationQueueTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
