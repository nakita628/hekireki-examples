package teable.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class ComputedUpdateDeadLetterSeedEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ComputedUpdateDeadLetterSeedEntity>(ComputedUpdateDeadLetterSeedTable)

    val taskId by ComputedUpdateDeadLetterSeedTable.taskId
    val tableId by ComputedUpdateDeadLetterSeedTable.tableId
    val recordId by ComputedUpdateDeadLetterSeedTable.recordId
    val task by ComputedUpdateDeadLetterEntity referencedOn ComputedUpdateDeadLetterSeedTable.taskId
}
