package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class SsoVerifiedDomainHolderEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SsoVerifiedDomainHolderEntity>(SsoVerifiedDomainHolderTable)

    val domain by SsoVerifiedDomainHolderTable.domain
    val connectionId by SsoVerifiedDomainHolderTable.connectionId
    var organizationId by SsoVerifiedDomainHolderTable.organizationId
}
