package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class WatchlistAuditEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, WatchlistAuditEntity>(WatchlistAuditTable)

    var type by WatchlistAuditTable.type
    var value by WatchlistAuditTable.value
    var description by WatchlistAuditTable.description
    var action by WatchlistAuditTable.action
    var changedAt by WatchlistAuditTable.changedAt
    var changedByUserId by WatchlistAuditTable.changedByUserId
    var watchlistId by WatchlistAuditTable.watchlistId
    var watchlist by WatchlistEntity optionalReferencedOn WatchlistAuditTable.watchlistId
}
