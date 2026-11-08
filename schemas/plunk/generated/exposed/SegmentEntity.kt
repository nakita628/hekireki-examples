package plunk.models

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

    var name by SegmentTable.name
    var description by SegmentTable.description
    var type by SegmentTable.type
    var condition by SegmentTable.condition
    var trackMembership by SegmentTable.trackMembership
    var memberCount by SegmentTable.memberCount
    var project by ProjectEntity referencedOn SegmentTable.projectId
    var projectId by SegmentTable.projectId
    val memberships by SegmentMembershipEntity referrersOn SegmentMembershipTable.segmentId
    val campaigns by CampaignEntity optionalReferrersOn CampaignTable.segmentId
    var createdAt by SegmentTable.createdAt
    var updatedAt by SegmentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SegmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
