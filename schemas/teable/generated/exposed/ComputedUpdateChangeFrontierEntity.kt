package teable.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class ComputedUpdateChangeFrontierEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ComputedUpdateChangeFrontierEntity>(ComputedUpdateChangeFrontierTable)

    val scopeId by ComputedUpdateChangeFrontierTable.scopeId
    val kind by ComputedUpdateChangeFrontierTable.kind
    val tableId by ComputedUpdateChangeFrontierTable.tableId
    val recordId by ComputedUpdateChangeFrontierTable.recordId
    val fieldId by ComputedUpdateChangeFrontierTable.fieldId
}
