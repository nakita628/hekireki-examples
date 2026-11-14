package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AnnotationScoreEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnnotationScoreEntity>(AnnotationScoreTable)

    var name by AnnotationScoreTable.name
    var projectId by AnnotationScoreTable.projectId
    var createdAt by AnnotationScoreTable.createdAt
    var updatedAt by AnnotationScoreTable.updatedAt
    var deletedAt by AnnotationScoreTable.deletedAt
    var description by AnnotationScoreTable.description
    var active by AnnotationScoreTable.active
    var dataType by AnnotationScoreTable.dataType
    var options by AnnotationScoreTable.options1
    var defaultValue by AnnotationScoreTable.defaultValue
    var global by AnnotationScoreTable.global

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AnnotationScoreTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
