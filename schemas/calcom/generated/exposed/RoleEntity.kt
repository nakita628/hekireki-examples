package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RoleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RoleEntity>(RoleTable)

    var name by RoleTable.name
    var color by RoleTable.color
    var description by RoleTable.description
    var teamId by RoleTable.teamId
    var team by TeamEntity optionalReferencedOn RoleTable.teamId
    val permissions by RolePermissionEntity referrersOn RolePermissionTable.roleId
    val memberships by MembershipEntity optionalReferrersOn MembershipTable.customRoleId
    var createdAt by RoleTable.createdAt
    var updatedAt by RoleTable.updatedAt
    var type by RoleTable.type

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RoleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
