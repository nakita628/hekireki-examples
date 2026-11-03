package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MembershipInvitationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MembershipInvitationEntity>(MembershipInvitationTable)

    var email by MembershipInvitationTable.email
    var orgId by MembershipInvitationTable.orgId
    var organization by OrganizationEntity referencedOn MembershipInvitationTable.orgId
    var orgRole by MembershipInvitationTable.orgRole
    var projectId by MembershipInvitationTable.projectId
    var project by ProjectEntity optionalReferencedOn MembershipInvitationTable.projectId
    var projectRole by MembershipInvitationTable.projectRole
    var invitedByUserId by MembershipInvitationTable.invitedByUserId
    var invitedByUser by UserEntity optionalReferencedOn MembershipInvitationTable.invitedByUserId
    var createdAt by MembershipInvitationTable.createdAt
    var updatedAt by MembershipInvitationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MembershipInvitationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
