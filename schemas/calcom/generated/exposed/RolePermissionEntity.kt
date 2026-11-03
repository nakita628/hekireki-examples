package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RolePermissionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RolePermissionEntity>(RolePermissionTable)

    var roleId by RolePermissionTable.roleId
    var role by RoleEntity referencedOn RolePermissionTable.roleId
    var resource by RolePermissionTable.resource
    var action by RolePermissionTable.action
    var createdAt by RolePermissionTable.createdAt
}
