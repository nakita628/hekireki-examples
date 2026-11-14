package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SsoBreakGlassBindingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SsoBreakGlassBindingEntity>(SsoBreakGlassBindingTable)

    var organizationId by SsoBreakGlassBindingTable.organizationId
    var userId by SsoBreakGlassBindingTable.userId
    var grantedByUserId by SsoBreakGlassBindingTable.grantedByUserId
    var grantedAt by SsoBreakGlassBindingTable.grantedAt
    var expiresAt by SsoBreakGlassBindingTable.expiresAt
    var supersededAt by SsoBreakGlassBindingTable.supersededAt
    var renewedFromId by SsoBreakGlassBindingTable.renewedFromId
    var warnedDays by SsoBreakGlassBindingTable.warnedDays
    var createdAt by SsoBreakGlassBindingTable.createdAt
}
