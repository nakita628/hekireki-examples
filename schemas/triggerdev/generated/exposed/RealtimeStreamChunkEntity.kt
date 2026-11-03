package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RealtimeStreamChunkEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RealtimeStreamChunkEntity>(RealtimeStreamChunkTable)

    var key by RealtimeStreamChunkTable.key
    var value by RealtimeStreamChunkTable.value
    var sequence by RealtimeStreamChunkTable.sequence
    var runId by RealtimeStreamChunkTable.runId
    var createdAt by RealtimeStreamChunkTable.createdAt
}
