package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SegmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SegmentEntity>(SegmentTable)

    var createdAt by SegmentTable.createdAt
    var updatedAt by SegmentTable.updatedAt
    var title by SegmentTable.title
    var description by SegmentTable.description
    var isPrivate by SegmentTable.isPrivate
    var filters by SegmentTable.filters
    var workspace by WorkspaceEntity referencedOn SegmentTable.workspaceId
    var workspaceId by SegmentTable.workspaceId
    val surveys by SurveyEntity optionalReferrersOn SurveyTable.segmentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SegmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
