package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RecordTrashEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RecordTrashEntity>(RecordTrashTable)

    var tableId by RecordTrashTable.tableId
    var recordId by RecordTrashTable.recordId
    var snapshot by RecordTrashTable.snapshot
    var createdTime by RecordTrashTable.createdTime
    var createdBy by RecordTrashTable.createdBy
    var reason by RecordTrashTable.reason
    var recordCreatedTime by RecordTrashTable.recordCreatedTime
    var recordCreatedBy by RecordTrashTable.recordCreatedBy
    var recordLastModifiedTime by RecordTrashTable.recordLastModifiedTime
    var recordLastModifiedBy by RecordTrashTable.recordLastModifiedBy
    var operationId by RecordTrashTable.operationId
}
