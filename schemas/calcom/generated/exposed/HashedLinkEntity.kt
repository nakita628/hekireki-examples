package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class HashedLinkEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, HashedLinkEntity>(HashedLinkTable)

    var link by HashedLinkTable.link
    var eventTypeId by HashedLinkTable.eventTypeId
    var eventType by EventTypeEntity referencedOn HashedLinkTable.eventTypeId
    var expiresAt by HashedLinkTable.expiresAt
    var maxUsageCount by HashedLinkTable.maxUsageCount
    var usageCount by HashedLinkTable.usageCount
}
