package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MembershipEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, MembershipEntity>(MembershipTable)

    var teamId by MembershipTable.teamId
    var userId by MembershipTable.userId
    var accepted by MembershipTable.accepted
    var role by MembershipTable.role
    var customRoleId by MembershipTable.customRoleId
    var customRole by RoleEntity optionalReferencedOn MembershipTable.customRoleId
    var team by TeamEntity referencedOn MembershipTable.teamId
    var user by UserEntity referencedOn MembershipTable.userId
    val attributeToUser by AttributeToUserEntity referrersOn AttributeToUserTable.memberId
    var createdAt by MembershipTable.createdAt
    var updatedAt by MembershipTable.updatedAt
    val host by HostEntity optionalReferrersOn HostTable.memberId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MembershipTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
