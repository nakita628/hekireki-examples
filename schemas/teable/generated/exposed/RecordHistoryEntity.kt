package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RecordHistoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RecordHistoryEntity>(RecordHistoryTable)

    var tableId by RecordHistoryTable.tableId
    var recordId by RecordHistoryTable.recordId
    var fieldId by RecordHistoryTable.fieldId
    var before by RecordHistoryTable.before
    var after by RecordHistoryTable.after
    var createdTime by RecordHistoryTable.createdTime
    var createdBy by RecordHistoryTable.createdBy
}
