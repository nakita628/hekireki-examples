package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserFilterSegmentPreferenceEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, UserFilterSegmentPreferenceEntity>(UserFilterSegmentPreferenceTable)

    var userId by UserFilterSegmentPreferenceTable.userId
    var tableIdentifier by UserFilterSegmentPreferenceTable.tableIdentifier
    var segmentId by UserFilterSegmentPreferenceTable.segmentId
    var systemSegmentId by UserFilterSegmentPreferenceTable.systemSegmentId
    var createdAt by UserFilterSegmentPreferenceTable.createdAt
    var updatedAt by UserFilterSegmentPreferenceTable.updatedAt
    var user by UserEntity referencedOn UserFilterSegmentPreferenceTable.userId
    var segment by FilterSegmentEntity optionalReferencedOn UserFilterSegmentPreferenceTable.segmentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserFilterSegmentPreferenceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
