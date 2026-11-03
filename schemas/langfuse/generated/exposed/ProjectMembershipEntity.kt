package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProjectMembershipEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ProjectMembershipEntity>(ProjectMembershipTable)

    var orgMembershipId by ProjectMembershipTable.orgMembershipId
    var organizationMembership by OrganizationMembershipEntity referencedOn ProjectMembershipTable.orgMembershipId
    val projectId by ProjectMembershipTable.projectId
    val project by ProjectEntity referencedOn ProjectMembershipTable.projectId
    val userId by ProjectMembershipTable.userId
    val user by UserEntity referencedOn ProjectMembershipTable.userId
    var role by ProjectMembershipTable.role
    var createdAt by ProjectMembershipTable.createdAt
    var updatedAt by ProjectMembershipTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectMembershipTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
