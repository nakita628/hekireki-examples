package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationMembershipEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationMembershipEntity>(OrganizationMembershipTable)

    var orgId by OrganizationMembershipTable.orgId
    var organization by OrganizationEntity referencedOn OrganizationMembershipTable.orgId
    var userId by OrganizationMembershipTable.userId
    var user by UserEntity referencedOn OrganizationMembershipTable.userId
    var role by OrganizationMembershipTable.role
    var createdAt by OrganizationMembershipTable.createdAt
    var updatedAt by OrganizationMembershipTable.updatedAt
    val projectMemberships by ProjectMembershipEntity referrersOn ProjectMembershipTable.orgMembershipId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationMembershipTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
