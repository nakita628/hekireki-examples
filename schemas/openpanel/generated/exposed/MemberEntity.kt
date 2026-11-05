package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class MemberEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, MemberEntity>(MemberTable)

    var role by MemberTable.role
    var email by MemberTable.email
    var userId by MemberTable.userId
    var user by UserEntity optionalReferencedOn MemberTable.userId
    var invitedById by MemberTable.invitedById
    var invitedBy by UserEntity optionalReferencedOn MemberTable.invitedById
    var organizationId by MemberTable.organizationId
    var organization by OrganizationEntity referencedOn MemberTable.organizationId
    var projectScope by MemberTable.projectScope
    val projects by MemberProjectEntity referrersOn MemberProjectTable.memberId
    var meta by MemberTable.meta
    var createdAt by MemberTable.createdAt
    var updatedAt by MemberTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MemberTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
