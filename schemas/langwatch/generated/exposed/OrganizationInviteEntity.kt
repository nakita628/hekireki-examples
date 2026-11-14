package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationInviteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationInviteEntity>(OrganizationInviteTable)

    var email by OrganizationInviteTable.email
    var inviteCode by OrganizationInviteTable.inviteCode
    var expiration by OrganizationInviteTable.expiration
    var status by OrganizationInviteTable.status
    var organizationId by OrganizationInviteTable.organizationId
    var teamIds by OrganizationInviteTable.teamIds
    var teamAssignments by OrganizationInviteTable.teamAssignments
    var role by OrganizationInviteTable.role
    var requestedBy by OrganizationInviteTable.requestedBy
    var subscriptionId by OrganizationInviteTable.subscriptionId
    var acceptedByUserId by OrganizationInviteTable.acceptedByUserId
    var acceptedViaIdentifierId by OrganizationInviteTable.acceptedViaIdentifierId
    var createdAt by OrganizationInviteTable.createdAt
    var updatedAt by OrganizationInviteTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationInviteTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
