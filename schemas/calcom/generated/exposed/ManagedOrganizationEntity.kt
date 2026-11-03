package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ManagedOrganizationEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, ManagedOrganizationEntity>(ManagedOrganizationTable)

    val managedOrganization by TeamEntity referencedOn ManagedOrganizationTable.id
    var managerOrganizationId by ManagedOrganizationTable.managerOrganizationId
    var managerOrganization by TeamEntity referencedOn ManagedOrganizationTable.managerOrganizationId
    var createdAt by ManagedOrganizationTable.createdAt
}
