package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class WatchlistEventAuditEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, WatchlistEventAuditEntity>(WatchlistEventAuditTable)

    var watchlistId by WatchlistEventAuditTable.watchlistId
    var eventTypeId by WatchlistEventAuditTable.eventTypeId
    var actionTaken by WatchlistEventAuditTable.actionTaken
    var timestamp by WatchlistEventAuditTable.timestamp
}
