package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SupportAccessRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SupportAccessRequestEntity>(SupportAccessRequestTable)

    var organization by OrganizationEntity referencedOn SupportAccessRequestTable.organizationId
    var organizationId by SupportAccessRequestTable.organizationId
    var requestedBy by UserEntity optionalReferencedOn SupportAccessRequestTable.requestedById
    var requestedById by SupportAccessRequestTable.requestedById
    var reason by SupportAccessRequestTable.reason
    var status by SupportAccessRequestTable.status
    var approvedBy by UserEntity optionalReferencedOn SupportAccessRequestTable.approvedById
    var approvedById by SupportAccessRequestTable.approvedById
    var approvedAt by SupportAccessRequestTable.approvedAt
    var expiresAt by SupportAccessRequestTable.expiresAt
    var cancelledById by SupportAccessRequestTable.cancelledById
    var cancelledAt by SupportAccessRequestTable.cancelledAt
    var createdAt by SupportAccessRequestTable.createdAt
    var updatedAt by SupportAccessRequestTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SupportAccessRequestTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
