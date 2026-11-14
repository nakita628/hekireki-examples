package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OrganizationSeatLeaseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationSeatLeaseEntity>(OrganizationSeatLeaseTable)

    var holder by OrganizationSeatLeaseTable.holder
    var expiresAt by OrganizationSeatLeaseTable.expiresAt
    var createdAt by OrganizationSeatLeaseTable.createdAt
}
