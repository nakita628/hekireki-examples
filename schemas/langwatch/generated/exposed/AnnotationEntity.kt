package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AnnotationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnnotationEntity>(AnnotationTable)

    var projectId by AnnotationTable.projectId
    var traceId by AnnotationTable.traceId
    var comment by AnnotationTable.comment
    var isThumbsUp by AnnotationTable.isThumbsUp
    var userId by AnnotationTable.userId
    var createdAt by AnnotationTable.createdAt
    var updatedAt by AnnotationTable.updatedAt
    var email by AnnotationTable.email
    var scoreOptions by AnnotationTable.scoreOptions
    var expectedOutput by AnnotationTable.expectedOutput
    var anchorKind by AnnotationTable.anchorKind
    var anchorId by AnnotationTable.anchorId
    var anchorPath by AnnotationTable.anchorPath

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AnnotationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
