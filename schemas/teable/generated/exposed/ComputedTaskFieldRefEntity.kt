package teable.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class ComputedTaskFieldRefEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ComputedTaskFieldRefEntity>(ComputedTaskFieldRefTable)

    val taskId by ComputedTaskFieldRefTable.taskId
    val fieldId by ComputedTaskFieldRefTable.fieldId
    var tableId by ComputedTaskFieldRefTable.tableId
    var baseId by ComputedTaskFieldRefTable.baseId
    var wasProcessing by ComputedTaskFieldRefTable.wasProcessing
    var createdAt by ComputedTaskFieldRefTable.createdAt
}
