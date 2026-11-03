package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RecordRemovalTombstoneEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RecordRemovalTombstoneEntity>(RecordRemovalTombstoneTable)

    var tableId by RecordRemovalTombstoneTable.tableId
    var recordId by RecordRemovalTombstoneTable.recordId
    var type by RecordRemovalTombstoneTable.type
    var createdTime by RecordRemovalTombstoneTable.createdTime
}
