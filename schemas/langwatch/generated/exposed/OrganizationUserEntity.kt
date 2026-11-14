package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationUserEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<OrganizationUserEntity>(OrganizationUserTable)

    val userId by OrganizationUserTable.userId
    val organizationId by OrganizationUserTable.organizationId
    var role by OrganizationUserTable.role
    var createdAt by OrganizationUserTable.createdAt
    var updatedAt by OrganizationUserTable.updatedAt
    var departmentId by OrganizationUserTable.departmentId
    var disabledAt by OrganizationUserTable.disabledAt
    var pendingSsoGrantId by OrganizationUserTable.pendingSsoGrantId
    var membershipStamp by OrganizationUserTable.membershipStamp
    var fullSeatSince by OrganizationUserTable.fullSeatSince
    var seatPendingSince by OrganizationUserTable.seatPendingSince

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationUserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
