package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SsoVerifiedDomainEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SsoVerifiedDomainEntity>(SsoVerifiedDomainTable)

    var organizationId by SsoVerifiedDomainTable.organizationId
}
