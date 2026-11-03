package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrgMemberInviteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrgMemberInviteEntity>(OrgMemberInviteTable)

    var token by OrgMemberInviteTable.token
    var email by OrgMemberInviteTable.email
    var role by OrgMemberInviteTable.role
    var rbacRoleId by OrgMemberInviteTable.rbacRoleId
    var organization by OrganizationEntity referencedOn OrgMemberInviteTable.organizationId
    var organizationId by OrgMemberInviteTable.organizationId
    var inviter by UserEntity referencedOn OrgMemberInviteTable.inviterId
    var inviterId by OrgMemberInviteTable.inviterId
    var createdAt by OrgMemberInviteTable.createdAt
    var updatedAt by OrgMemberInviteTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrgMemberInviteTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
