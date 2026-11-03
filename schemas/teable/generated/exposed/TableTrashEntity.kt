package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TableTrashEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TableTrashEntity>(TableTrashTable)

    var tableId by TableTrashTable.tableId
    var resourceType by TableTrashTable.resourceType
    var snapshot by TableTrashTable.snapshot
    var createdTime by TableTrashTable.createdTime
    var createdBy by TableTrashTable.createdBy
}
