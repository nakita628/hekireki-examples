package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class SegmentMembershipEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SegmentMembershipEntity>(SegmentMembershipTable)

    val contact by ContactEntity referencedOn SegmentMembershipTable.contactId
    val contactId by SegmentMembershipTable.contactId
    val segment by SegmentEntity referencedOn SegmentMembershipTable.segmentId
    val segmentId by SegmentMembershipTable.segmentId
    var enteredAt by SegmentMembershipTable.enteredAt
    var exitedAt by SegmentMembershipTable.exitedAt
    var createdAt by SegmentMembershipTable.createdAt
    var updatedAt by SegmentMembershipTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SegmentMembershipTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
