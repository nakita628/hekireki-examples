package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ContactEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ContactEntity>(ContactTable)

    var email by ContactTable.email
    var data by ContactTable.data
    var subscribed by ContactTable.subscribed
    var snoozedUntil by ContactTable.snoozedUntil
    var project by ProjectEntity referencedOn ContactTable.projectId
    var projectId by ContactTable.projectId
    val emails by EmailEntity referrersOn EmailTable.contactId
    val workflowExecutions by WorkflowExecutionEntity referrersOn WorkflowExecutionTable.contactId
    val events by EventEntity optionalReferrersOn EventTable.contactId
    val segmentMemberships by SegmentMembershipEntity referrersOn SegmentMembershipTable.contactId
    val throttleClaims by ThrottleClaimEntity optionalReferrersOn ThrottleClaimTable.contactId
    var createdAt by ContactTable.createdAt
    var updatedAt by ContactTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ContactTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
